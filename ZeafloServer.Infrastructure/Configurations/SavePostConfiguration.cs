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
    public sealed class SavePostConfiguration : IEntityTypeConfiguration<SavePost>
    {
        public void Configure(EntityTypeBuilder<SavePost> builder)
        {
            builder.HasKey(s => s.SavePostId);

            builder.Property(s => s.UserId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(s => s.PostId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(s => s.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.HasOne(u => u.User)
                .WithMany(sp => sp.SavePosts)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_SavePost_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Post)
                .WithMany(sp => sp.SavePosts)
                .HasForeignKey(p => p.PostId)
                .HasConstraintName("FK_SavePost_Post_PostId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
