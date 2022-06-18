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
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", Constants.DbConstants.DefaultTableSchema);
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.DomainEvents);
            builder.Property(e => e.Name).HasMaxLength(20);
            builder.Property(e => e.Remark).HasMaxLength(100);
        }
    }
}
