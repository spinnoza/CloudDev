using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ZeroStack.DeviceCenter.Application.Services.Permissions
{
    /// <summary>
    /// 授权策略提供者,用户将以不同维度(role ,user,department ...)的provider对权限进行检查
    /// </summary>
    public interface IPermissionValueProvider
    {
        string Name { get; }

        Task<PermissionGrantResult> CheckAsync(ClaimsPrincipal principal, PermissionDefinition permission);

        Task<MultiplePermissionGrantResult> CheckAsync(ClaimsPrincipal principal, List<PermissionDefinition> permissions);
    }
}
