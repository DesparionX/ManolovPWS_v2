using ManolovPWS_v2.Domain.Abstractions;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Domain.Contracts.Factories
{
    public interface IFactory<TEntity, TKey>
        where TEntity : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Task<ITaskResult<TEntity>> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
