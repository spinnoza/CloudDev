using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Aggregates.ProductAggregate;

namespace ZeroStack.DeviceCenter.Infrastructure.EntityConfigurations.Products
{
    public class DeviceTagEntityTypeConfiguration : IEntityTypeConfiguration<DeviceTag>
    {
        public void Configure(EntityTypeBuilder<DeviceTag> builder)
        {
            builder.ToTable("DeviceTags", Constants.DbConstants.DefaultTableSchema);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(20);
        }
    }
}
