using Application.Dtos.User;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Authentication.Sesion.Commands.RefreshTokenCommand
{
    public class RefreshTokenCommand : IRequest<Response<AuthenticationResponseDto>>
    {
        public string RefreshToken { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
    }
}
