using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ZeroStack.DeviceCenter.API.Extensions.Tenants;
using ZeroStack.DeviceCenter.Application;
using ZeroStack.DeviceCenter.Domain;
using ZeroStack.DeviceCenter.Domain.Repositories;
using ZeroStack.DeviceCenter.Infrastructure;

namespace ZeroStack.DeviceCenter.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddDomainLayer().AddInfrastructureLayer(Configuration).AddApplicationLayer(); 

            services.AddTenantMiddleware();

            services.AddControllers()
                //添加FluentValidation模型验证
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()))
                //添加模型验证本地化
                .AddDataAnnotationsLocalization();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ZeroStack.DeviceCenter.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region 本地化支持
            string[] supportedCultures = new[] { "zh-CN", "en-US" };
            RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions
            {
                ApplyCurrentCultureToResponseHeaders = false
            };
            localizationOptions.SetDefaultCulture(supportedCultures.First()).AddSupportedCultures(supportedCultures).AddSupportedUICultures(supportedCultures);
            app.UseRequestLocalization(localizationOptions);
            #endregion


            app.UseTenantMiddleware();

            #region 针对FluentValidation,针对 Display 和 DisplayName 特性做多语言处理
            IStringLocalizerFactory? localizerFactory = app.ApplicationServices.GetService<IStringLocalizerFactory>();

            FluentValidation.ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, lambdaExpression) =>
            {
                string? displayName = string.Empty;

                DisplayAttribute? displayColumnAttribute = memberInfo.GetCustomAttributes(true).OfType<DisplayAttribute>().FirstOrDefault();

                if (displayColumnAttribute is not null)
                {
                    displayName = displayColumnAttribute.Name;
                }

                DisplayNameAttribute? displayNameAttribute = memberInfo.GetCustomAttributes(true).OfType<DisplayNameAttribute>().FirstOrDefault();

                if (displayNameAttribute is not null)
                {
                    displayName = displayNameAttribute.DisplayName;
                }

                if (!string.IsNullOrWhiteSpace(displayName) && localizerFactory is not null)
                {
                    return localizerFactory.Create(type)[displayName];
                }

                if (!string.IsNullOrWhiteSpace(displayName))
                {
                    return displayName;
                }

                return memberInfo.Name;
            };
            #endregion


            //用IDataSeedProvider 初始化数据
            using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
            {
                var dataSeedProviders = serviceScope.ServiceProvider.GetServices<IDataSeedProvider>();

                foreach (IDataSeedProvider dataSeedProvider in dataSeedProviders)
                {
                    dataSeedProvider.SeedAsync(serviceScope.ServiceProvider).Wait();
                }
            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZeroStack.DeviceCenter.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
