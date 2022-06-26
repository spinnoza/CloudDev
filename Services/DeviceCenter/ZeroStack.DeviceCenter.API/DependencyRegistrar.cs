using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ZeroStack.DeviceCenter.API.Extensions.Hosting;
using ZeroStack.DeviceCenter.API.Extensions.Tenants;

namespace ZeroStack.DeviceCenter.API
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddWebApiLayer(this IServiceCollection services)
        {
            services.AddTransient<IStartupFilter, CustomStartupFilter>();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddTenantMiddleware();

            return services;
        }
    }
}
