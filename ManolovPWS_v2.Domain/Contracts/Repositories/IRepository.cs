using ManolovPWS_v2.Domain.Abstractions;
using ManolovPWS_v2.Shared.Abstractions;

namespace ManolovPWS_v2.Domain.Contracts.Repositories
{
    public interface IRepository<TEntity, TKey> 
        where TEntity : IEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<ITaskResult<TEntity>> GetByIdAsync(TKey id);
        Task<ITaskResult> SaveAsync(TEntity entity);
        Task<ITaskResult> RemoveAsync(TKey id);
    }
}
