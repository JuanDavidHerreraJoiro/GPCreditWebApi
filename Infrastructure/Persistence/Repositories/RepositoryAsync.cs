using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class RepositoryAsync<TEntity> : IRepositoryAsync<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _entity;

        public RepositoryAsync(ApplicationDbContext context)
        {
            _context = context;
            _entity = context.Set<TEntity>();
        }

        public void Delete(TEntity data)
        {
            _entity.Remove(data);
        }

        public async Task<List<TEntity>> GetAllAsync() => await _entity.ToListAsync();

        public async Task<TEntity?> GetByIdAsync(int id) => await _entity.FindAsync(id)!;

        public async Task<TEntity> SavedAsync(TEntity data)
        {
            var entity = await _entity.AddAsync(data);
            await _context.SaveChangesAsync();

            return entity.Entity;
        }

        public async Task Update(TEntity data)
        {
            _entity.Attach(data);
            _context.Entry(data).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
