using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using ZeroStack.DeviceCenter.Application.Models.Generics;
using ZeroStack.DeviceCenter.Application.Models.Projects;
using ZeroStack.DeviceCenter.Application.Services.Generics;
using ZeroStack.DeviceCenter.Application.Services.Products;
using ZeroStack.DeviceCenter.Application.Services.Projects;

namespace ZeroStack.DeviceCenter.Application
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddDomainEvents();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
            services.AddTransient(typeof(ICrudApplicationService<int, ProjectGetResponseModel, PagedRequestModel, ProjectGetResponseModel, ProjectCreateOrUpdateRequestModel, ProjectCreateOrUpdateRequestModel>), typeof(ProjectApplicationService));
            services.AddTransient<IProductApplicationService, ProductApplicationService>();

            return services;
        }
    }
}
