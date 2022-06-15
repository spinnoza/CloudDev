using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Aggregates.ProjectAggregate;

namespace ZeroStack.DeviceCenter.Domain.Services.Projects
{
    public interface IProjectDomainService
    {
        Task<Project> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Project>> ListAllAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Project>> ListAsync(Expression<Func<Project, bool>> where, int pageNumber, CancellationToken cancellationToken = default);

        Task<Project> AddAsync(Project entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(Project entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(Project entity, CancellationToken cancellationToken = default);

        Task<int> CountAsync(Expression<Func<Project, bool>> where, CancellationToken cancellationToken = default);

        Task<Project> FirstAsync(Expression<Func<Project, bool>> where, CancellationToken cancellationToken = default);

        Task<Project> FirstOrDefaultAsync(Expression<Func<Project, bool>> where, CancellationToken cancellationToken = default);
    }
}
