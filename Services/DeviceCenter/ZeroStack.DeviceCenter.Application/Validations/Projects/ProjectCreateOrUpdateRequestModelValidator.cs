using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Reflection;
using ZeroStack.DeviceCenter.Application.Models.Projects;

namespace ZeroStack.DeviceCenter.Application.Validations.Projects
{
    public class ProjectCreateOrUpdateRequestModelValidator : AbstractValidator<ProjectCreateOrUpdateRequestModel>
    {
        public ProjectCreateOrUpdateRequestModelValidator(IStringLocalizerFactory factory)
        {

            //这种本地化器的查找会直接通过传入的路径进行查找资源文件(此类情况一般是多个模型验证共用一个本地化资源文件)
            IStringLocalizer _localizer1 = factory.Create("Models.Projects.ProjectCreateOrUpdateRequestModel", Assembly.GetExecutingAssembly().ToString());

            //这种本地化器的查找会在资源文件的配置路径中按照模型配置的路径进行查找资源文件
            IStringLocalizer _localizer2 = factory.Create(typeof(ProjectCreateOrUpdateRequestModel));

            RuleFor(m => m.Name).Length(4, 7).WithMessage((m, p) => _localizer1["LengthValidator", _localizer1["ProjectName"], 5, 7, p.Length]);
            RuleFor(m => m.Name).NotNull().NotEmpty().Length(3, 8).WithName(_localizer2["ProjectName"]);
        }
    }
}
