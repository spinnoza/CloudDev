
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using ZeroStack.DeviceCenter.Domain.Aggregates.TenantAggregate;
using ZeroStack.DeviceCenter.Domain.Repositories;
using ZeroStack.DeviceCenter.Domain.Services.Projects;

namespace ZeroStack.DeviceCenter.Domain
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddDomainLayer(this IServiceCollection services)
        {
            services.AddTransient<IProjectDomainService, ProjectDomainService>();

            //遍历所有的程序集,注入IDataSeedProvider 实现类
            var dataSeedProviders = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.ExportedTypes).Where(t => t.IsAssignableTo(typeof(IDataSeedProvider)) && t.IsClass);
            dataSeedProviders.ToList().ForEach(t => services.AddTransient(typeof(IDataSeedProvider), t));

            services.AddSingleton<ICurrentTenantAccessor, CurrentTenantAccessor>();
            services.AddTransient<ICurrentTenant, CurrentTenant>();

            return services;
        }
    }
}
