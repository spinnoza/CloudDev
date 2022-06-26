using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZeroStack.DeviceCenter.Application;
using ZeroStack.DeviceCenter.Domain;
using ZeroStack.DeviceCenter.Infrastructure;

[assembly: HostingStartup(typeof(ZeroStack.DeviceCenter.API.Extensions.Hosting.CustomHostingStartup))]
namespace ZeroStack.DeviceCenter.API.Extensions.Hosting
{
    public class CustomHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

                services.AddDomainLayer()
                        .AddInfrastructureLayer(configuration)
                        .AddApplicationLayer()
                        .AddWebApiLayer();
            });
        }
    }
}
