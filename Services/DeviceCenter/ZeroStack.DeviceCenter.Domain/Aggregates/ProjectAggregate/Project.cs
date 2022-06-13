using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Entities;

namespace ZeroStack.DeviceCenter.Domain.Aggregates.ProjectAggregate
{
    public class Project : BaseAggregateRoot<int>
    {
        public string Name { get; set; } = null!;

        public DateTimeOffset CreationTime { get; set; } = DateTimeOffset.Now;

        public List<ProjectGroup> Groups { get; set; } = null!;
    }
}
