using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Aggregates.ProjectAggregate;
using ZeroStack.DeviceCenter.Domain.Specifications;

namespace ZeroStack.DeviceCenter.Domain.Services.Projects
{
    public class ProjectPagingSpecification : Specification<Project>
    {
        public ProjectPagingSpecification(int pageNumber, int pageSize)
        {
            Query.Where(e => e.CreationTime > DateTimeOffset.Now.AddDays(-1));
            Query.Include(e => e.Groups);
            Query.Include(e => e.Groups).ThenInclude(e => e.Count);
            Query.OrderBy(e => e.Id);
            Query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
