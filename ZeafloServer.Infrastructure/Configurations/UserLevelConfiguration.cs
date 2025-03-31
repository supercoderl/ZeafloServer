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
    public sealed class UserLevelConfiguration : IEntityTypeConfiguration<UserLevel>
    {
        public void Configure(EntityTypeBuilder<UserLevel> builder)
        {
            builder.HasKey(u => u.UserLevelId);

            builder.Property(u  => u.UserId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(u => u.MemberShipLevelId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(u => u.ZeafloPoint)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(u => u.AssignedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.HasOne(u => u.User)
                .WithMany(ul => ul.UserLevels)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_UserLevel_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.MemberShipLevel)
                .WithMany(ul => ul.UserLevels)
                .HasForeignKey(m => m.MemberShipLevelId)
                .HasConstraintName("FK_UserLevel_MemberShipLevel_MemberShipLevelId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
