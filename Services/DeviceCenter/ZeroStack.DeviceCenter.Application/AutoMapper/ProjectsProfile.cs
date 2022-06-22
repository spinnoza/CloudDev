using AutoMapper;
using ZeroStack.DeviceCenter.Application.Models.Projects;
using ZeroStack.DeviceCenter.Domain.Aggregates.ProjectAggregate;

namespace ZeroStack.DeviceCenter.Application.AutoMapper
{
    public class ProjectsProfile : Profile
    {
        public ProjectsProfile()
        {
            CreateMap<Project, ProjectGetResponseModel>();
            CreateMap<ProjectCreateOrUpdateRequestModel, Project>();
        }
    }
}
