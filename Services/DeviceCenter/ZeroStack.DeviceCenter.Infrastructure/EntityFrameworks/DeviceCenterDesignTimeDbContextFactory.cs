using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace ZeroStack.DeviceCenter.Infrastructure.EntityFrameworks
{
    /// <summary>
    /// Dbcontext 设计时工厂,可以在ef 所在类库下进行迁移,不需要借助启动项目
    /// </summary>
    public class DeviceCenterDesignTimeDbContextFactory : IDesignTimeDbContextFactory<DeviceCenterDbContext>
    {
        public DeviceCenterDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DeviceCenterDbContext>();
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));
            optionsBuilder.UseMySql(@"server=192.168.89.129;database=DeviceCenter;user=root;password=123456", serverVersion);

            return new DeviceCenterDbContext(optionsBuilder.Options);
        }
    }
}
