using FluentValidation;
using ZeroStack.DeviceCenter.Application.Models.Projects;

namespace ZeroStack.DeviceCenter.Application.Validations.Projects
{
    public class ProjectCreateOrUpdateRequestModelValidator : AbstractValidator<ProjectCreateOrUpdateRequestModel>
    {
        public ProjectCreateOrUpdateRequestModelValidator()
        {
            RuleFor(m => m.Name).NotNull().NotEmpty().Length(3, 8).WithName("ProjectName");
        }
    }
}
