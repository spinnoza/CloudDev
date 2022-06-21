using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZeroStack.DeviceCenter.Domain.Aggregates.ProductAggregate;

namespace ZeroStack.DeviceCenter.Infrastructure.EntityConfigurations.Products
{
    public class DeviceEntityTypeConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Devices", Constants.DbConstants.DefaultTableSchema);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(20);
            builder.Property(e => e.SerialNo).IsRequired().HasMaxLength(36);

            var converter = new ValueConverter<GeoCoordinate, string>(v => v, v => (GeoCoordinate)v);
            builder.Property(e => e.Coordinate).HasConversion(converter);

            builder.OwnsOne(e => e.Address, da => da.ToTable("DeviceAddresses"));
        }
    }
}
