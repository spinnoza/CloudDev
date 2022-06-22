using System;

namespace ZeroStack.DeviceCenter.Application.Models.Projects
{
    public class ProjectCreateOrUpdateRequestModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTimeOffset CreationTime { get; set; }
    }
}
