using MediatR;

namespace ZeroStack.DeviceCenter.Domain.Events.Projects
{
    public class ProjectDeletedDomainEvent : INotification
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; } = null!;
    }
}
