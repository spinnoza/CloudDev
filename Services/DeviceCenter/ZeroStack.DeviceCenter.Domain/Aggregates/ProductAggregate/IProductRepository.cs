using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Repositories;

namespace ZeroStack.DeviceCenter.Domain.Aggregates.ProductAggregate
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> Add(Product product);
    }
}
