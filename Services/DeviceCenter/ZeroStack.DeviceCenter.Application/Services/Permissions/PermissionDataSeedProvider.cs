using System;
using System.Linq;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Application.Models.Permissions;
using ZeroStack.DeviceCenter.Domain.Repositories;

namespace ZeroStack.DeviceCenter.Application.Services.Permissions
{
    public class PermissionDataSeedProvider : IDataSeedProvider
    {
        private readonly IPermissionApplicationService _permissionService;

        private readonly IPermissionDefinitionManager _permissionDefinitionManager;

        public PermissionDataSeedProvider(IPermissionApplicationService permissionService, IPermissionDefinitionManager permissionDefinitionManager)
        {
            _permissionService = permissionService;
            _permissionDefinitionManager = permissionDefinitionManager;
        }

        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var permissionNames = _permissionDefinitionManager.GetPermissions().Where(p => !p.AllowedProviders.Any() || p.AllowedProviders.Contains(RolePermissionValueProvider.ProviderName)).Select(p => p.Name).ToArray();
            var permissionModels = Array.ConvertAll(permissionNames, pn => new PermissionUpdateRequestModel { Name = pn, IsGranted = true });

            await _permissionService.UpdateAsync(RolePermissionValueProvider.ProviderName, "admin", permissionModels);
        }
    }
}
