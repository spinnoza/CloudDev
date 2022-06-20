using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroStack.DeviceCenter.Domain.Events.Projects
{
    public class ProjectDeletedDomainEvent : INotification
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; } = null!;
    }
}
