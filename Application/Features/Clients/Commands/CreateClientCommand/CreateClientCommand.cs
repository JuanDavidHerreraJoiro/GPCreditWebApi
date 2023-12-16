using Application.Base.Commands;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Clients.Commands.CreateClientCommand
{
    public class CreateClientCommand : CreateUserCommand, IRequest<Response<int>>
    {
        public string Identification { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
