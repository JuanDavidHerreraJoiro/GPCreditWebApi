using Application.Dtos.Client;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Clients.Queries.GetAllClientsQuery
{
    public class GetAllClientsQuery : IRequest<Response<List<ClientDto>>>
    {
    }
}
