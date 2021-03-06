using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Entities;

namespace ZeroStack.DeviceCenter.Domain.Aggregates.ProjectAggregate
{
    public class ProjectGroup: BaseEntity<int>
    {
        public string Name { get; set; } = null!;

        public int ProjectId { get; set; }

        public Project Project { get; set; } = null!;

        public ProjectGroup Parent { get; set; } = null!;

        public List<ProjectGroup> Children { get; set; } = null!;

        public int? ParentId { get; set; }

        public string? Remark { get; set; }

        public int? Sorting { get; set; }

        public DateTimeOffset CreationTime { get; set; } = DateTimeOffset.Now;
    }
}
