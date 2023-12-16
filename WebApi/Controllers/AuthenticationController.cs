using Application.Dtos.User;
using Application.Features.Authentication.Sesion.Commands.AuthenticateCommand;
using Application.Features.Authentication.Sesion.Commands.RefreshTokenCommand;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseApiController
    {
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequestDto request)
        {
            var response = await Mediator!.Send(new AuthenticateCommand
            {
                UserName = request.UserName,
                Password = request.Password,
                IpAddress = GenerateIpAddress()
            });

            //SetTokenCookie(response.Data!.RefreshToken);

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            var refreshToken = Request.Cookies["refreshToken"]!;
            var response = await Mediator!.Send(new RefreshTokenCommand
            {
                RefreshToken = refreshToken,
                IpAddress = GenerateIpAddress()
            });

            //SetTokenCookie(response.Data!.RefreshToken);

            return Ok(response);
        }

        private string GenerateIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return Request.Headers["X-Forwarded-For"];
            }
            else
            {
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }

        private void SetTokenCookie(string token)
        {
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(2)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
