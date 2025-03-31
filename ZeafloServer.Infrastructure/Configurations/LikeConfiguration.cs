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
    public sealed class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(l => l.LikeId);

            builder.Property(l => l.UserId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(l => l.PostId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(l => l.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.HasOne(u => u.User)
                .WithMany(l => l.Likes)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_Like_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Post)
                .WithMany(l => l.Likes)
                .HasForeignKey(p => p.PostId)
                .HasConstraintName("FK_Like_Post_PostId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
