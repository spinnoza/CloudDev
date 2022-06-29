using System.Collections.Generic;

namespace ZeroStack.DeviceCenter.Application.Models.Permissions
{
    public class PermissionListResponseModel
    {
        public string EntityDisplayName { get; set; } = null!;

        public List<PermissionGroupModel> Groups { get; set; } = null!;
    }
}
