using System.Collections.Generic;

namespace ZeroStack.DeviceCenter.Application.Models.Permissions
{
    public class PermissionGrantModel
    {
        public string Name { get; set; } = null!;

        public string DisplayName { get; set; } = null!;

        public string ParentName { get; set; } = null!;

        public bool IsGranted { get; set; }

        public List<string> AllowedProviders { get; set; } = null!;
    }
}
