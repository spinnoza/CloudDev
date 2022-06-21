﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZeroStack.DeviceCenter.Domain.Aggregates.TenantAggregate;

namespace ZeroStack.DeviceCenter.Infrastructure.EntityConfigurations.Tenants
{
    public class TenantConnectionStringEntityTypeConfiguration : IEntityTypeConfiguration<TenantConnectionString>
    {
        public void Configure(EntityTypeBuilder<TenantConnectionString> builder)
        {
            builder.ToTable("TenantConnectionStrings", Constants.DbConstants.DefaultTableSchema);

            builder.HasKey(x => new { x.TenantId, x.Name });

            builder.Property(e => e.Name).IsRequired().HasMaxLength(64);
            builder.Property(e => e.Value).IsRequired().HasMaxLength(1024);
        }
    }
}
