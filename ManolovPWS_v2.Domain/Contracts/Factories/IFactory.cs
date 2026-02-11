using ManolovPWS_v2.Domain.Abstractions;

namespace ManolovPWS_v2.Domain.Contracts.Factories
{
    public interface IFactory<TEntity, TKey>
        where TEntity : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Task<TEntity?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
