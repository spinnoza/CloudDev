using MediatR;
using ZeroStack.DeviceCenter.Domain.Aggregates.ProductAggregate;

namespace ZeroStack.DeviceCenter.Domain.Events.Products
{
    public class ProductCreatedDomainEvent : INotification
    {
        public Product Product { get; set; } = null!;

        public ProductCreatedDomainEvent(Product product) => Product = product;
    }
}
