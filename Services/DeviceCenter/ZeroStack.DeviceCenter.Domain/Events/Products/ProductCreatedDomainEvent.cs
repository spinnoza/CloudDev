using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
