using AutoMapper;
using ZeroStack.DeviceCenter.Application.Models.Generics;
using ZeroStack.DeviceCenter.Application.Models.Projects;
using ZeroStack.DeviceCenter.Application.Services.Generics;
using ZeroStack.DeviceCenter.Domain.Aggregates.ProjectAggregate;
using ZeroStack.DeviceCenter.Domain.Repositories;

namespace ZeroStack.DeviceCenter.Application.Services.Projects
{
    public class ProjectApplicationService : CrudApplicationService<Project, int, ProjectGetResponseModel, PagedRequestModel, ProjectGetResponseModel, ProjectCreateOrUpdateRequestModel, ProjectCreateOrUpdateRequestModel>
    {
        public ProjectApplicationService(IRepository<Project, int> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
