using BugStore.Models;

namespace BugStore.Services
{
    public interface IBaseService<T> where T : Entity
    {
        Task<Result<Exception, T?>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Result<Exception, IEnumerable<T>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<Exception, bool>> AddAsync(T entity, CancellationToken cancellationToken);
        Task<Result<Exception, bool>> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task<Result<Exception, bool>> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
