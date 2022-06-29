using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Application.Models.Permissions;

namespace ZeroStack.DeviceCenter.Application.Services.Permissions
{
    public interface IPermissionApplicationService
    {
        Task<PermissionListResponseModel> GetAsync([NotNull] string providerName, [NotNull] string providerKey);

        Task UpdateAsync([NotNull] string providerName, [NotNull] string providerKey, IEnumerable<PermissionUpdateRequestModel> requestModels);
    }
}
