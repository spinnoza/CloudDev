using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Entities;
using ZeroStack.DeviceCenter.Domain.UnitOfWork;

namespace ZeroStack.DeviceCenter.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> InsertAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        Task DeleteAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        Task DeleteAsync([NotNull] Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default);

        Task<TEntity> UpdateAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetListAsync(int pageNumber, int pageSize, Expression<Func<TEntity, object>> sorting, bool includeDetails = false, CancellationToken cancellationToken = default);

        IQueryable<TEntity> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors);

        Task<TEntity> FindAsync([NotNull] Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default);

        Task<TEntity> GetAsync([NotNull] Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default);

        IQueryable<TEntity> Query { get; }

        IUnitOfWork UnitOfWork { get; }

        IAsyncQueryableProvider AsyncExecuter { get; }
    }

    public interface IRepository<TEntity, TKey> : IRepository<TEntity> where TEntity : BaseEntity<TKey>
    {
        Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default);

        Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default);

        Task<TEntity> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default);
    }
}
