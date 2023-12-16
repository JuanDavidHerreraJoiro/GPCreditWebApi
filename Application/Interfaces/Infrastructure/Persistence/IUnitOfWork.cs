using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Domain.Entities;

namespace Application.Interfaces.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        IRepositoryAsync<Client> Clients { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
