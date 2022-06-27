using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ZeroStack.DeviceCenter.API.Extensions.Authorization;
using ZeroStack.DeviceCenter.API.Extensions.Hosting;
using ZeroStack.DeviceCenter.API.Extensions.Tenants;
using ZeroStack.DeviceCenter.Application.Services.Permissions;

namespace ZeroStack.DeviceCenter.API
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddWebApiLayer(this IServiceCollection services)
        {
            services.AddTransient<IStartupFilter, CustomStartupFilter>();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddTenantMiddleware();

            services.AddHttpContextAccessor();


            services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();

            services.AddTransient<IPermissionChecker, PermissionChecker>();

            services.AddTransient<IAuthorizationHandler, PermissionRequirementHandler>();

            return services;
        }
    }
}
