using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Application.Models.Generics;

namespace ZeroStack.DeviceCenter.Application.Services.Generics
{
    public interface ICrudApplicationService<TKey, TGetResponseModel, TGetListRequestModel, TGetListResponseModel, TCreateRequestModel, TUpdateRequestModel>
    {
        Task<TGetResponseModel> CreateAsync(TCreateRequestModel requestModel);

        Task DeleteAsync(TKey id);

        Task<TGetResponseModel> UpdateAsync(TUpdateRequestModel requestModel);

        Task<TGetResponseModel> GetAsync(TKey id);

        Task<PagedResponseModel<TGetListResponseModel>> GetListAsync(TGetListRequestModel requestModel);
    }
}
