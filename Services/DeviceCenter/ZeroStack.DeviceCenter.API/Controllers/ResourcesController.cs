using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace ZeroStack.DeviceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private IStringLocalizerFactory _localizerFactory;

        public ResourcesController(IStringLocalizerFactory localizerFactory)
        {
            _localizerFactory = localizerFactory;
        }

        [HttpGet]
        public string GetHelloWorld()
        {
            IStringLocalizer stringLocalizer = _localizerFactory.Create("Welcome", Assembly.GetExecutingAssembly().ToString());

            return stringLocalizer["HelloWorld"].Value;
        }
    }
}
