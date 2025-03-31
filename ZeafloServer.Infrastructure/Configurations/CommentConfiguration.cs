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
    public sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.CommentId);

            builder.Property(c => c.PostId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(c => c.UserId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(c => c.Content)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.HasOne(u => u.User)
                .WithMany(c => c.Comments)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_Comment_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Post)
                .WithMany(c => c.Comments)
                .HasForeignKey(p => p.PostId)
                .HasConstraintName("FK_Comment_Post_PostId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
