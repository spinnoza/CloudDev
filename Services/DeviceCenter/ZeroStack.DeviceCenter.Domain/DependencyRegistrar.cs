using Microsoft.Extensions.DependencyInjection;
using ZeroStack.DeviceCenter.Domain.Services.Projects;

namespace ZeroStack.DeviceCenter.Domain
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddDomainLayer(this IServiceCollection services)
        {
            services.AddTransient<IProjectDomainService, ProjectDomainService>();

            return services;
        }
    }
}
