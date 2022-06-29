using System.Collections.Generic;

namespace ZeroStack.DeviceCenter.Application.Models.Permissions
{
    public class PermissionGroupModel
    {
        public string Name { get; set; } = null!;

        public string DisplayName { get; set; } = null!;

        public List<PermissionGrantModel> Permissions { get; set; } = null!;
    }
}
