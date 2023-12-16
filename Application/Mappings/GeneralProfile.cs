using Application.Base.Commands;
using Application.Dtos.Client;
using Application.Dtos.User;
using Application.Features.Clients.Commands.CreateClientCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {
            #region Entities to Dtos
            CreateMap<Client, ClientDto>();
            #endregion

            #region Commands to Entities
            CreateMap<CreateClientCommand, Client>();
            #endregion

            #region CreateUserCommand to RegisterUserRequestDto
            CreateMap<CreateUserCommand, RegisterUserRequestDto>();
            #endregion
        }
    }
}
