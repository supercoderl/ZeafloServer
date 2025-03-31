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
    public sealed class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
    {
        public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
        {
            builder.HasKey(p => p.PasswordResetTokenId);

            builder.Property(p => p.UserId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(p => p.Code)
                .IsRequired()
                .HasColumnType("varchar(6)");

            builder.Property(p => p.ExpiresAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.Property(p => p.AttemptCount)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(p => p.IsUsed)
                .IsRequired()
                .HasColumnType("boolean");

            builder.HasOne(u => u.User)
                .WithMany(p => p.PasswordResetTokens)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_PasswordResetToken_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
