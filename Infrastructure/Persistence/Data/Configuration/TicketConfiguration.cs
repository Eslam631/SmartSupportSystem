using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    internal class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
           builder.ToTable("Tickets");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(1000);
            builder.Property(t => t.Status)
                .IsRequired();
            builder.HasOne(t => t.Department)
                .WithMany()
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.SuperAgent)
                .WithMany()
                .HasForeignKey(t => t.AgentId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(t => t.CreatedAt)
                .IsRequired();
            builder.Property(t => t.UpdatedBy)
                .HasMaxLength(100);
            builder.Property(t => t.UpdatedAt);
        }
    }
}
