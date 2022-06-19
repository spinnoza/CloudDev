using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Aggregates.ProjectAggregate;
using ZeroStack.DeviceCenter.Domain.Repositories;

namespace ZeroStack.DeviceCenter.Domain.Services.Projects
{
    public class ProjectDataSeedProvider : IDataSeedProvider
    {
        private readonly IRepository<Project, int> _projectRepository;

        public ProjectDataSeedProvider(IRepository<Project, int> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            if (await _projectRepository.GetCountAsync() <= 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    var project = new Project { Name = $"Project{i}" };
                    await _projectRepository.InsertAsync(project);
                }

                await _projectRepository.UnitOfWork.SaveChangesAsync();
            }
        }
    }
}
