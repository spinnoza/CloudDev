using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ZeroStack.DeviceCenter.Application.Services.Permissions
{
    public interface IPermissionDefinitionManager
    {
        PermissionDefinition Get([NotNull] string name);

        PermissionDefinition? GetOrNull([NotNull] string name);

        IReadOnlyList<PermissionDefinition> GetPermissions();

        IReadOnlyList<PermissionGroupDefinition> GetGroups();
    }
}
