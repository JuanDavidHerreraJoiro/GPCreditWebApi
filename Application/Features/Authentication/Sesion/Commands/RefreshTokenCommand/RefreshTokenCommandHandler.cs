using Application.Dtos.User;
using Application.Interfaces.Infrastructure.Identity;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Authentication.Sesion.Commands.RefreshTokenCommand
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Response<AuthenticationResponseDto>>
    {
        private readonly IAccountService _accountService;

        public RefreshTokenCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Response<AuthenticationResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _accountService.RefreshTokenAsync(request.RefreshToken, request.IpAddress);
        }
    }
}
