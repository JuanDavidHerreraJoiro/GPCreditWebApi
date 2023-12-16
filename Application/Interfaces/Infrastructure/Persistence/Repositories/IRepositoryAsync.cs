namespace Application.Interfaces.Infrastructure.Persistence.Repositories
{
    public interface IRepositoryAsync<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity> SavedAsync(TEntity data);
        Task Update(TEntity data);
        void Delete(TEntity data);
    }
}
