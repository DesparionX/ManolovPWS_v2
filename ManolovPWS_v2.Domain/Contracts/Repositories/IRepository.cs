using ManolovPWS_v2.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Contracts.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
