using ManolovPWS_v2.Domain.Abstractions;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Domain.Contracts.Repositories
{
    public interface IRepository<TEntity, TKey> 
        where TEntity : IEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        Task<ITaskResult<IReadOnlyList<TEntity>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ITaskResult<TEntity>> FindByIdAsync(TKey id, CancellationToken cancellationToken = default);
        Task<ITaskResult> SaveAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<ITaskResult> RemoveAsync(TKey id, CancellationToken cancellationToken = default);
    }
}
