using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Data.Repositories
{
    public class BaseRepository<T> where T : Entity
    {
        public readonly DbSet<T> _dbSet;

        protected BaseRepository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public async Task UpdateAsync(T entity) => await _dbSet.Where(x => x.Id == entity.Id)
                                                               .ExecuteUpdateAsync(setters => setters.SetProperty(
                                                                   x => x,
                                                                   entity));        
            
        public async Task DeleteAsync(Guid id) => await _dbSet.Where(x => x.Id == id)
                                                              .ExecuteDeleteAsync();
    }
}
