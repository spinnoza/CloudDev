using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZeroStack.DeviceCenter.Domain.Aggregates.ProjectAggregate;

namespace ZeroStack.DeviceCenter.Infrastructure.EntityConfigurations.Projects
{
    public class ProjectGroupEntityTypeConfiguration : IEntityTypeConfiguration<ProjectGroup>
    {
        public void Configure(EntityTypeBuilder<ProjectGroup> builder)
        {
            builder.ToTable("ProjectGroups", Constants.DbConstants.DefaultTableSchema);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(20);
            builder.Property(e => e.Remark).HasMaxLength(100);
        }
    }
}
