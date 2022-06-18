using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Infrastructure.EntityFrameworks;
using Microsoft.EntityFrameworkCore;
using ZeroStack.DeviceCenter.Infrastructure.Constants;
using System.Reflection;
using ZeroStack.DeviceCenter.Domain.Repositories;
using MySql.EntityFrameworkCore.Extensions;

namespace ZeroStack.DeviceCenter.Infrastructure
{
    /// <summary>
    /// 每一个程序集都会创建依赖注入帮助类,用来管理涉及到自己关联服务的注入行为
    /// </summary>
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkMySQL();

            services.AddDbContextPool<DeviceCenterDbContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseMySQL(configuration.GetConnectionString(DbConstants.DefaultConnectionStringName), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                    //sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });

                //设置 Entityframework的服务提供者(容器),如果未指定任何服务提供程序，EF 将创建和管理服务提供程序。
                optionsBuilder.UseInternalServiceProvider(serviceProvider);
            });

            //为当前容器注入DbContextFactory,DbContextFactory可以用来解析DbContext,而不是由容器提供
            services.AddPooledDbContextFactory<DeviceCenterDbContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseMySQL(configuration.GetConnectionString(DbConstants.DefaultConnectionStringName), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                    //sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });

                optionsBuilder.UseInternalServiceProvider(serviceProvider);
            });

             services.AddTransient(typeof(IRepository<>), typeof(DeviceCenterEfCoreRepository<>));
             services.AddTransient(typeof(IRepository<,>), typeof(DeviceCenterEfCoreRepository<,>));

            return services;
        }
    }
}
