using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using ZeroStack.DeviceCenter.Application.Models.Generics;
using ZeroStack.DeviceCenter.Application.Models.Projects;
using ZeroStack.DeviceCenter.Application.PermissionProviders;
using ZeroStack.DeviceCenter.Application.Services.Generics;
using ZeroStack.DeviceCenter.Application.Services.Permissions;
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
            services.AddAuthorization();

            //替换FluentValidation的模型验证多语言管理器为自己的
            ValidatorOptions.Global.LanguageManager = new Extensions.Validators.CustomLanguageManager();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
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


        private static IServiceCollection AddAuthorization(this IServiceCollection services)
        {

            services.AddDistributedMemoryCache();
            services.AddTransient<IPermissionStore, PermissionStore>();

            services.AddSingleton<IPermissionDefinitionProvider, CustomPermissionDefinitionProvider>();
            services.AddSingleton<IPermissionDefinitionManager, PermissionDefinitionManager>();

            services.AddSingleton<IPermissionValueProvider, UserPermissionValueProvider>();
            services.AddSingleton<IPermissionValueProvider, RolePermissionValueProvider>();

            return services;
        }
    }
}
