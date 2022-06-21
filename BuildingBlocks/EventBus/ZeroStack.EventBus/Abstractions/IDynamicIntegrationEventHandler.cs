using System.Threading.Tasks;

namespace ZeroStack.EventBus.Abstractions
{
    public interface IDynamicIntegrationEventHandler : IIntegrationEventHandler
    {
        Task HandleAsync(dynamic eventData);
    }
}
