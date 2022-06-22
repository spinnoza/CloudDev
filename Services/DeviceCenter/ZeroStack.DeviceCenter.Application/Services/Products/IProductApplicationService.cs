using System;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Application.Models.Generics;
using ZeroStack.DeviceCenter.Application.Models.Products;
using ZeroStack.DeviceCenter.Application.Services.Generics;
using ZeroStack.DeviceCenter.Domain.Aggregates.ProductAggregate;

namespace ZeroStack.DeviceCenter.Application.Services.Products
{
    public interface IProductApplicationService : ICrudApplicationService<Guid, ProductGetResponseModel, PagedRequestModel, ProductGetResponseModel, ProductCreateOrUpdateRequestModel, ProductCreateOrUpdateRequestModel>
    {
        Task<Product> GetByName(string productName);
    }
}
