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
    public sealed class UserStatusConfiguration : IEntityTypeConfiguration<UserStatus>
    {
        public void Configure(EntityTypeBuilder<UserStatus> builder)
        {
            builder.HasKey(u => u.UserStatusId);

            builder.Property(u => u.UserId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(u => u.IsOnline)
                .IsRequired()
                .HasColumnType("boolean");

            builder.Property(u => u.LastSeen)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.HasOne(u => u.User)
                .WithMany(us => us.UserStatuses)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_UserStatus_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
