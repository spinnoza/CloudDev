using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Events.Projects;

namespace ZeroStack.DeviceCenter.Application.DomainEventHandlers.Projects.ProjectDeleted
{
    public class ProjectDeletedSendSmsDomainEventHandler : INotificationHandler<ProjectDeletedDomainEvent>
    {
        public Task Handle(ProjectDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
