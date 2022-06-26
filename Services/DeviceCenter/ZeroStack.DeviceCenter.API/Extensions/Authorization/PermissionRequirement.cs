using Microsoft.AspNetCore.Authorization;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ZeroStack.DeviceCenter.API.Extensions.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string PermissionName { get; }

        public PermissionRequirement([NotNull] string permissionName)
        {
            if (string.IsNullOrWhiteSpace(permissionName))
            {
                throw new ArgumentException(permissionName);
            }

            PermissionName = permissionName;
        }
    }
}
