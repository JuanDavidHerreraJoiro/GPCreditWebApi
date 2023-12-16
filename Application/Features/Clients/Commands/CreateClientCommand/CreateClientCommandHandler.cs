using Application.Dtos.User;
using Application.Interfaces.Infrastructure.Identity;
using Application.Interfaces.Infrastructure.Persistence;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Clients.Commands.CreateClientCommand
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Client> _clientRepository;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public CreateClientCommandHandler(IRepositoryAsync<Client> clientRepository, IAccountService accountService, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<RegisterUserRequestDto>(request);
            var client = _mapper.Map<Client>(request);

            user.Role = Roles.Client;

            var idUser = await _accountService.RegisterAsync(user, "");

            client.UserId = idUser;

            var clientRegistred = await _clientRepository.SavedAsync(client);

            return new Response<int>(clientRegistred.Id);
        }
    }
}
