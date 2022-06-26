using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ZeroStack.DeviceCenter.Application.Services.Permissions
{
    public class PermissionDefinition
    {
        public string Name { get; } = null!;

        public PermissionDefinition? Parent { get; private set; }

        public List<string> AllowedProviders { get; set; } = new List<string>();

        public string? DisplayName { get; set; }

        private readonly List<PermissionDefinition> _children = new List<PermissionDefinition>();

        public IReadOnlyList<PermissionDefinition> Children => _children.ToImmutableList();

        public bool IsEnabled { get; set; }

        protected internal PermissionDefinition([NotNull] string name, string? displayName = null, bool isEnabled = true)
        {
            Name = name;
            DisplayName = displayName;
            IsEnabled = isEnabled;
        }

        public virtual PermissionDefinition AddChild([NotNull] string name, string? displayName = null, bool isEnabled = true)
        {
            var child = new PermissionDefinition(name, displayName, isEnabled) { Parent = this };
            _children.Add(child);
            return child;
        }

        public virtual PermissionDefinition WithProviders(params string[] providers)
        {
            if (providers != null && providers.Any())
            {
                AllowedProviders.AddRange(providers);
            }

            return this;
        }
    }
}
