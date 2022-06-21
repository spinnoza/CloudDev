using System;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Application.Models.Generics;
using ZeroStack.DeviceCenter.Application.Models.Products;

namespace ZeroStack.DeviceCenter.Application.Services.Products
{
    public interface IProductApplicationService
    {
        Task<ProductGetResponseModel> CreateAsync(ProductCreateOrUpdateRequestModel requestModel);

        Task DeleteAsync(Guid id);

        Task<ProductGetResponseModel> UpdateAsync(ProductCreateOrUpdateRequestModel requestModel);

        Task<ProductGetResponseModel> GetAsync(Guid id);

        Task<PagedResponseModel<ProductGetResponseModel>> GetListAsync(PagedRequestModel requestModel);
    }
}
