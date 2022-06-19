using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Repositories;

namespace ZeroStack.DeviceCenter.Domain.Aggregates.ProductAggregate
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> Add(Product product);
    }
}
