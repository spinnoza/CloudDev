using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using ZeroStack.DeviceCenter.Application.Services.Products;

namespace ZeroStack.DeviceCenter.Application
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddDomainEvents();
            services.AddApplicationServices();

            return services;
        }

        private static IServiceCollection AddDomainEvents(this IServiceCollection services)
        {
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IProductApplicationService, ProductApplicationService>();

            return services;
        }
    }
}
