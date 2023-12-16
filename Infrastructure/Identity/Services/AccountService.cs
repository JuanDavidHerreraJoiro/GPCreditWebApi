using Application.Dtos.User;
using Application.Exceptions;
using Application.Interfaces.Infrastructure.Identity;
using Application.Wrappers;
using Domain.Settings;
using Infrastructure.Identity.Interfaces;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtHelper;
        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshTokenRepositoryAsync _refreshTokenRepository;

        public AccountService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtHelper,
            IOptions<JwtSettings> jwtSettings,
            IRefreshTokenRepositoryAsync refreshTokenRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtHelper = jwtHelper;
            _jwtSettings = jwtSettings.Value;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<Response<AuthenticationResponseDto>> AuthenticateAsync(AuthenticationRequestDto request, string ipAddress)
        {
            var user = await _userManager.FindByNameAsync(request.UserName) ?? throw new ApiException($"No hay una cuenta registrada con el usuario {request.UserName} ");
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new ApiException($"Las credenciales del usuario no son válidas");
            }

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            var role = rolesList.FirstOrDefault();

            var token = _jwtHelper.GenerateJwtToken(user, role);

            var refreshToken = _jwtHelper.GenerateRefreshToken(ipAddress);
            refreshToken.ApplicationUserId = user.Id;

            await RemoveOldRefreshTokens(user.Id);

            await _refreshTokenRepository.SaveTokenAsync(refreshToken);

            var response = MapAuthenticationResponse(user, token, refreshToken.Token, role);

            return new Response<AuthenticationResponseDto>(response, $"Usuario autenticado {user.UserName}");
        }

        public async Task<string> RegisterAsync(RegisterUserRequestDto request, string origin)
        {
            var userNameUsed = await _userManager.FindByNameAsync(request.UserName);

            if (userNameUsed != null)
            {
                throw new ApiException($"El nombre de usuario {request.UserName} ya fue registrado. ");
            }

            var emailUsed = await _userManager.FindByEmailAsync(request.Email);

            if (emailUsed != null)
            {
                throw new ApiException($"El correo {request.Email} ya fue registrado. ");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.UserName,
                EmailConfirmed = true,
                PhoneNumber = request.Phone,
                PhoneNumberConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, request.Role.ToString());
                return user.Id;

            }
            else
            {
                throw new ApiException($"{result.Errors}");
            }
        }

        public async Task<Response<AuthenticationResponseDto>> RefreshTokenAsync(string token, string ipAddress)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(token);

            if (refreshToken.IsRevoked)
            {
                // revoke all descendant tokens in case this token has been compromised
                await RevokeDescendantRefreshTokens(refreshToken, ipAddress, $"Intento de reutilización de token revocado: {token}");
            }

            if (!refreshToken.IsActive)
                throw new ApiException("Invalid token");

            // replace old refresh token with a new one (rotate token)
            var newRefreshToken = RotateRefreshToken(refreshToken, ipAddress);

            // remove old refresh tokens from user
            await RemoveOldRefreshTokens(refreshToken.ApplicationUserId);
            var user = await _userManager.FindByIdAsync(refreshToken.ApplicationUserId);
            
            newRefreshToken.ApplicationUserId = user.Id;

            await _refreshTokenRepository.SaveTokenAsync(newRefreshToken);

            // generate new jwt
            var roles = await _userManager.GetRolesAsync(user!);
            var role = roles.FirstOrDefault()!;
            var jwtToken = _jwtHelper.GenerateJwtToken(user, role);

            var response = MapAuthenticationResponse(user, jwtToken, newRefreshToken.Token, role);

            return new Response<AuthenticationResponseDto>(response, $"Usuario autenticado {user.UserName}");
        }

        private static AuthenticationResponseDto MapAuthenticationResponse(ApplicationUser user, string jwtToken, string refreshToken, string role = "")
        {
            AuthenticationResponseDto response = new()
            {
                //Id = user.Id,
                Token = jwtToken,
                Email = user.Email!,
                UserName = user.UserName!,
                IsVerified = true,
                Role = role,
                RefreshToken = refreshToken
            };

            return response;
        }

        private RefreshToken RotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = _jwtHelper.GenerateRefreshToken(ipAddress);
            RevokeRefreshToken(refreshToken, ipAddress, "Reemplazado por un nuevo token", newRefreshToken.Token);
            return newRefreshToken;
        }

        private async Task RemoveOldRefreshTokens(string userId)
        {
            // remove old inactive refresh tokens from user based on TTL in app settings
            await _refreshTokenRepository.RemoveOldTokensAsync(userId, _jwtSettings.RefreshTokenTTL);
        }

        private async Task RevokeDescendantRefreshTokens(RefreshToken refreshToken, string ipAddress, string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = await _refreshTokenRepository.GetChildTokenAsync(refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    RevokeRefreshToken(childToken, ipAddress, reason);
                else
                    await RevokeDescendantRefreshTokens(childToken, ipAddress, reason);
            }
        }

        private static void RevokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }
    }
}
