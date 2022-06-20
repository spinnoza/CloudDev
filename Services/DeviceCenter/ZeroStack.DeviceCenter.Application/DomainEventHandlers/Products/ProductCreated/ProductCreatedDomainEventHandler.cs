using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Events.Products;

namespace ZeroStack.DeviceCenter.Application.DomainEventHandlers.Products.ProductCreated
{
    public class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreatedDomainEvent>
    {
        private readonly ILogger<ProductCreatedDomainEventHandler> _logger;

        public ProductCreatedDomainEventHandler(ILogger<ProductCreatedDomainEventHandler> logger) => _logger = logger;

        public async Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"ProductId={notification.Product.Id},ProductName={notification.Product.Name}");

            await Task.CompletedTask;
        }
    }
}
