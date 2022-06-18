using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Aggregates.ProjectAggregate;

namespace ZeroStack.DeviceCenter.Infrastructure.EntityConfigurations.Projects
{
    public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects", Constants.DbConstants.DefaultTableSchema);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(20);
        }
    }
}
