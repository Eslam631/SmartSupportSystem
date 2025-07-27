using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            builder.Property(b => b.UpdatedAt).IsRequired(false);
            builder.Property(b => b.IsDeleted).HasDefaultValue(false);
            builder.Property(b => b.Name).HasColumnType("nvarchar(20)")
                .IsRequired();
            builder.HasIndex(b => b.Name).IsUnique();
        }
    }
}
