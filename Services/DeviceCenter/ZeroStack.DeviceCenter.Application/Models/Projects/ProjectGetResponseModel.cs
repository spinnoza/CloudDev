using System;

namespace ZeroStack.DeviceCenter.Application.Models.Projects
{
    public class ProjectGetResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTimeOffset CreationTime { get; set; }
    }
}
