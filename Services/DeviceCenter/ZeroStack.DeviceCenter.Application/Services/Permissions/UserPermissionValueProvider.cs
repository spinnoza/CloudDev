﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ZeroStack.DeviceCenter.Application.Services.Permissions
{
    public class UserPermissionValueProvider : IPermissionValueProvider
    {
        public const string ProviderName = "User";

        private readonly IPermissionStore _permissionStore;

        public string Name => ProviderName;

        public UserPermissionValueProvider(IPermissionStore permissionStore)
        {
            _permissionStore = permissionStore;
        }

        public async Task<PermissionGrantResult> CheckAsync(ClaimsPrincipal principal, PermissionDefinition permission)
        {
            var userId = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
            {
                return PermissionGrantResult.Undefined;
            }

            return await _permissionStore.IsGrantedAsync(permission.Name, Name, userId) ? PermissionGrantResult.Granted : PermissionGrantResult.Undefined;
        }

        public async Task<MultiplePermissionGrantResult> CheckAsync(ClaimsPrincipal principal, List<PermissionDefinition> permissions)
        {
            var permissionNames = permissions.Select(x => x.Name).ToArray();

            var userId = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
            {
                return new MultiplePermissionGrantResult(permissionNames);
            }

            return await _permissionStore.IsGrantedAsync(permissionNames, Name, userId);
        }
    }
}
