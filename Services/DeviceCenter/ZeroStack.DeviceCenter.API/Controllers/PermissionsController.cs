using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Application.Models.Permissions;
using ZeroStack.DeviceCenter.Application.Services.Permissions;

namespace ZeroStack.DeviceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionApplicationService _permissionApplicationService;

        public PermissionsController(IPermissionApplicationService permissionApplicationService)
        {
            _permissionApplicationService = permissionApplicationService;
        }

        [HttpGet]
        public virtual Task<PermissionListResponseModel> GetAsync(string providerName, string providerKey)
        {
            return _permissionApplicationService.GetAsync(providerName, providerKey);
        }

        [HttpPut]
        public virtual Task UpdateAsync(string providerName, string providerKey, IEnumerable<PermissionUpdateRequestModel> model)
        {
            return _permissionApplicationService.UpdateAsync(providerName, providerKey, model);
        }
    }
}
