using Application.Dtos.User;
using Application.Wrappers;

namespace Application.Interfaces.Infrastructure.Identity
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponseDto>> AuthenticateAsync(AuthenticationRequestDto request, string ipAddress);
        Task<string> RegisterAsync(RegisterUserRequestDto request, string origin);
        Task<Response<AuthenticationResponseDto>> RefreshTokenAsync(string token, string ipAddress);
    }
}
