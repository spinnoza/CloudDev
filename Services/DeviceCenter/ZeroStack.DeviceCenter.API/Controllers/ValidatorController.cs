using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Application.Models.Projects;

namespace ZeroStack.DeviceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //模型验证器示例controller （基于FluentValidation）
    public class ValidatorController : ControllerBase
    {
        private readonly IValidator<ProjectCreateOrUpdateRequestModel> _validator;

        public ValidatorController(IValidator<ProjectCreateOrUpdateRequestModel> validator)
        {
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult<ProjectGetResponseModel>> Post([FromBody] ProjectCreateOrUpdateRequestModel value)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(value);

            if (validationResult.IsValid)
            {
                return await Task.FromResult(new ProjectGetResponseModel());
            }

            return BadRequest(validationResult);
        }
    }
}
