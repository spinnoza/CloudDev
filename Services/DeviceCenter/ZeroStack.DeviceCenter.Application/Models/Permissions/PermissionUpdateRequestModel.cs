namespace ZeroStack.DeviceCenter.Application.Models.Permissions
{
    public class PermissionUpdateRequestModel
    {
        public string Name { get; set; } = null!;

        public bool IsGranted { get; set; }
    }
}
