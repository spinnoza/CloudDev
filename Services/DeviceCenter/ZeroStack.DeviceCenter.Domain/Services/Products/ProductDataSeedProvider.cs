using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Aggregates.ProductAggregate;
using ZeroStack.DeviceCenter.Domain.Repositories;

namespace ZeroStack.DeviceCenter.Domain.Services.Products
{
    public class ProductDataSeedProvider : IDataSeedProvider
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductDataSeedProvider(IRepository<Product, Guid> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            if (await _productRepository.GetCountAsync() <= 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    var product = new Product { Name = $"Product{i}" };
                    await _productRepository.InsertAsync(product, true);
                }
            }
        }
    }
}
