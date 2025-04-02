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
    public sealed class ProcessingConfiguration : IEntityTypeConfiguration<Processing>
    {
        public void Configure(EntityTypeBuilder<Processing> builder)
        {
            builder.HasKey(p => p.ProcessingId);

            builder.Property(p => p.UserId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(p => p.Type)
                .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.Property(p => p.UpdatedAt)
                .HasColumnType("timestamp");

            builder.ToTable(tb => tb.HasCheckConstraint("CK_Processing_Type", "type IN ('Pending', 'InProgress', 'Completed', 'Failed')"));

            builder.HasOne(u => u.User)
                .WithOne(p => p.Processing)
                .HasForeignKey<Processing>(u => u.UserId)
                .HasConstraintName("FK_Processing_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
