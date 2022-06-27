using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Application.Services.Permissions;

namespace ZeroStack.DeviceCenter.API.Extensions.Authorization
{
    public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionChecker _permissionChecker;

        public PermissionRequirementHandler(IPermissionChecker permissionChecker)
        {
            _permissionChecker = permissionChecker;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (await _permissionChecker.IsGrantedAsync(context.User, requirement.PermissionName))
            {
                context.Succeed(requirement);
            }
        }
    }
}
