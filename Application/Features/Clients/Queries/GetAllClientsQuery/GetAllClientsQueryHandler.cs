using Application.Dtos.Client;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Clients.Queries.GetAllClientsQuery
{
    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, Response<List<ClientDto>>>
    {
        private readonly IRepositoryAsync<Client> _clientRepository;
        private readonly IMapper _mapper;

        public GetAllClientsQueryHandler(IRepositoryAsync<Client> clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<ClientDto>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _clientRepository.GetAllAsync();
            var cliensDto = _mapper.Map<List<Client>, List<ClientDto>>(clients);

            return new Response<List<ClientDto>>(cliensDto);
        }
    }
}
