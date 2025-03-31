using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Infrastructure.Configurations
{
    public sealed class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(r => r.ReportId);

            builder.Property(r => r.UserId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(r => r.PostId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(r => r.Reason)
                .IsRequired();

            builder.Property(r => r.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(r => r.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.ToTable(tb => tb.HasCheckConstraint("CK_Report_Status", "status IN ('Pending', 'Resolved', 'Rejected')"));

            builder.HasOne(u => u.User)
                .WithMany(r => r.Reports)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_Report_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Post)
                .WithMany(r => r.Reports)
                .HasForeignKey(p => p.PostId)
                .HasConstraintName("FK_Report_Post_PostId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
