using System.Threading.Tasks;
using ZeroStack.EventBus.Events;

namespace ZeroStack.EventBus.Abstractions
{
    public interface IIntegrationEventHandler { }

    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler where TIntegrationEvent : IntegrationEvent
    {
        Task HandleAsync(TIntegrationEvent @event);
    }
}
