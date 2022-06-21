using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Aggregates.ProductAggregate;
using ZeroStack.DeviceCenter.Infrastructure.EntityFrameworks;

namespace ZeroStack.DeviceCenter.Infrastructure.Repositories
{
    public class ProductRepository : EfCoreRepository<DeviceCenterDbContext, Product>, IProductRepository
    {
        public ProductRepository(DeviceCenterDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Product> Add(Product product)
        {
            await DbSet.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}
