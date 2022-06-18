using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.UnitOfWork;


namespace ZeroStack.DeviceCenter.Infrastructure.EntityFrameworks
{
    public class DeviceCenterDbContext : DbContext, IUnitOfWork
    {
        public DeviceCenterDbContext(DbContextOptions<DeviceCenterDbContext> options) : base(options)
        {

        }

        async Task IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken) => await base.SaveChangesAsync(cancellationToken);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
