using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace ZeroStack.DeviceCenter.Application.Services.Permissions
{
    public class PermissionGroupDefinition
    {
        public string Name { get; } = null!;

        public string? DisplayName { get; set; }

        private readonly List<PermissionDefinition> _permissions = new List<PermissionDefinition>();

        public IReadOnlyList<PermissionDefinition> Permissions => _permissions.ToImmutableList();

        protected internal PermissionGroupDefinition([NotNull] string name, string? displayName = null)
        {
            Name = name;
            DisplayName = displayName;
        }

        public virtual PermissionDefinition AddPermission([NotNull] string name, string? displayName = null, bool isEnabled = true)
        {
            var permission = new PermissionDefinition(name, displayName, isEnabled);
            _permissions.Add(permission);
            return permission;
        }

        public virtual List<PermissionDefinition> GetPermissionsWithChildren()
        {
            var permissions = new List<PermissionDefinition>();

            foreach (var permission in _permissions)
            {
                AddPermissionToListRecursively(permissions, permission);
            }

            return permissions;
        }

        private void AddPermissionToListRecursively(List<PermissionDefinition> permissions, PermissionDefinition permission)
        {
            permissions.Add(permission);

            foreach (var child in permission.Children)
            {
                AddPermissionToListRecursively(permissions, child);
            }
        }

        public PermissionDefinition GetPermissionOrNull([NotNull] string name)
        {
            return GetPermissionOrNullRecursively(Permissions, name);
        }

        private PermissionDefinition GetPermissionOrNullRecursively(IReadOnlyList<PermissionDefinition> permissions, string name)
        {
            foreach (var permission in permissions)
            {
                if (permission.Name == name)
                {
                    return permission;
                }

                var childPermission = GetPermissionOrNullRecursively(permission.Children, name);
                if (childPermission != null)
                {
                    return childPermission;
                }
            }

            return null!;
        }
    }
}
