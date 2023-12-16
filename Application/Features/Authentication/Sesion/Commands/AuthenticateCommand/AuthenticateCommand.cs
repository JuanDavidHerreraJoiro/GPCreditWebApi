using Application.Dtos.User;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Authentication.Sesion.Commands.AuthenticateCommand
{
    public class AuthenticateCommand : IRequest<Response<AuthenticationResponseDto>>
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
    }
}
