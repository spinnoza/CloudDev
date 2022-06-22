using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Application.Models.Generics;
using ZeroStack.DeviceCenter.Application.Models.Projects;
using ZeroStack.DeviceCenter.Application.Services.Generics;

namespace ZeroStack.DeviceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ICrudApplicationService<int, ProjectGetResponseModel, PagedRequestModel, ProjectGetResponseModel, ProjectCreateOrUpdateRequestModel, ProjectCreateOrUpdateRequestModel> _crudService;

        public ProjectsController(ICrudApplicationService<int, ProjectGetResponseModel, PagedRequestModel, ProjectGetResponseModel, ProjectCreateOrUpdateRequestModel, ProjectCreateOrUpdateRequestModel> crudService)
        {
            _crudService = crudService;
        }

        // GET: api/<ProjectsController>
        [HttpGet]
        public async Task<PagedResponseModel<ProjectGetResponseModel>> Get([FromQuery] PagedRequestModel model)
        {
            return await _crudService.GetListAsync(model);
        }

        // GET api/<ProjectsController>/5
        [HttpGet("{id}")]
        public async Task<ProjectGetResponseModel> Get(int id)
        {
            return await _crudService.GetAsync(id);
        }

        // POST api/<ProjectsController>
        [HttpPost]
        public async Task<ProjectGetResponseModel> Post([FromBody] ProjectCreateOrUpdateRequestModel value)
        {
            return await _crudService.CreateAsync(value);
        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{id}")]
        public async Task<ProjectGetResponseModel> Put(int id, [FromBody] ProjectCreateOrUpdateRequestModel value)
        {
            value.Id = id;
            return await _crudService.UpdateAsync(value);
        }

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _crudService.DeleteAsync(id);
        }
    }
}
