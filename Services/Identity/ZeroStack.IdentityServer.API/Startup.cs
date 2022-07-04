using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Reflection;
using ZeroStack.IdentityServer.API.Models;

namespace ZeroStack.IdentityServer.API
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
            services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            //services.AddControllersWithViews();


            var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));

            services.AddDbContext<ApplicationDbContext>(options =>
            {

                options.UseMySql(Configuration.GetConnectionString("Default"), serverVersion, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


            services.AddIdentityServer().AddAspNetIdentity<ApplicationUser>()
              .AddDeveloperSigningCredential()
              .AddConfigurationStore(options =>
              {
                  options.ConfigureDbContext = builder => builder.UseMySql(Configuration.GetConnectionString("Default"), serverVersion, sqlOptions =>
                  {
                      sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                      sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                      sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                  });
              })
              .AddOperationalStore(options =>
              {
                  options.ConfigureDbContext = builder => builder.UseMySql(Configuration.GetConnectionString("Default"), serverVersion, sqlOptions =>
                  {
                      sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                      sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                      sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                  });
              });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            string[] supportedCultures = new[] { "zh-CN", "en-US" };
            RequestLocalizationOptions localizationOptions = new()
            {
                ApplyCurrentCultureToResponseHeaders = false
            };
            localizationOptions.SetDefaultCulture(supportedCultures.First()).AddSupportedCultures(supportedCultures).AddSupportedUICultures(supportedCultures);
            app.UseRequestLocalization(localizationOptions);

            SampleDataSeed.SeedAsync(app).Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
