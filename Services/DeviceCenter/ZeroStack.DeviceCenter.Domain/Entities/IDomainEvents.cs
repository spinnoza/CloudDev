using MediatR;
using System.Collections.Generic;

namespace ZeroStack.DeviceCenter.Domain.Entities
{
    public interface IDomainEvents
    {
        IReadOnlyCollection<INotification> DomainEvents { get; }

        void AddDomainEvent(INotification eventItem);

        void RemoveDomainEvent(INotification eventItem);

        void ClearDomainEvents();
    }
}
