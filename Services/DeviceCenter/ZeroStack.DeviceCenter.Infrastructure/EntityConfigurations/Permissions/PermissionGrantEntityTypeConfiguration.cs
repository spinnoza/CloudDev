using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using ZeroStack.DeviceCenter.Domain.Aggregates.PermissionAggregate;

namespace ZeroStack.DeviceCenter.Infrastructure.EntityConfigurations.Permissions
{
    public class PermissionGrantEntityTypeConfiguration : IEntityTypeConfiguration<PermissionGrant>
    {
        public void Configure(EntityTypeBuilder<PermissionGrant> builder)
        {
            builder.ToTable("PermissionGrants", Constants.DbConstants.DefaultTableSchema);

            builder.HasKey(e => e.Id);

            foreach (IMutableEntityType entityType in builder.Metadata.Model.GetEntityTypes())
            {
                if (entityType.ClrType == typeof(PermissionGrant))
                {
                    foreach (IMutableProperty property in entityType.GetProperties().Where(p => p.ClrType == typeof(string)))
                    {
                        property.SetMaxLength(20);
                        builder.Property(property.Name).IsRequired(false);
                    }
                }
            }

            builder.Property(e => e.Name).IsRequired().HasMaxLength(128);

            builder.HasIndex(e => new { e.Name, e.ProviderName, e.ProviderKey });
        }
    }
}
