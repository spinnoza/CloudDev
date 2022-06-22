using AutoMapper;
using System;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Application.Models.Generics;
using ZeroStack.DeviceCenter.Application.Models.Products;
using ZeroStack.DeviceCenter.Application.Services.Generics;
using ZeroStack.DeviceCenter.Domain.Aggregates.ProductAggregate;
using ZeroStack.DeviceCenter.Domain.Repositories;

namespace ZeroStack.DeviceCenter.Application.Services.Products
{
    public class ProductApplicationService : CrudApplicationService<Product, Guid, ProductGetResponseModel, PagedRequestModel, ProductGetResponseModel, ProductCreateOrUpdateRequestModel, ProductCreateOrUpdateRequestModel>, IProductApplicationService
    {
        public ProductApplicationService(IRepository<Product, Guid> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public async Task<Product> GetByName(string productName)
        {
            return await Repository.GetAsync(p => p.Name == productName);
        }
    }
}
