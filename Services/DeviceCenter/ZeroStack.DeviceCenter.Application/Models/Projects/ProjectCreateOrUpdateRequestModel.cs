using System;
using System.ComponentModel.DataAnnotations;
namespace ZeroStack.DeviceCenter.Application.Models.Projects
{
    public class ProjectCreateOrUpdateRequestModel
    {
        public int Id { get; set; }
        [StringLength(8, ErrorMessage = "The {0} must be at least {2} characte long.", MinimumLength = 6)]
        [Display(Name = "ProjectName")]
        public string Name { get; set; } = null!;

        public DateTimeOffset CreationTime { get; set; }
    }
}
