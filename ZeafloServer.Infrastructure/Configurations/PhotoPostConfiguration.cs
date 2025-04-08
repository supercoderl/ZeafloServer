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
    public sealed class PhotoPostConfiguration : IEntityTypeConfiguration<PhotoPost>
    {
        public void Configure(EntityTypeBuilder<PhotoPost> builder)
        {
            builder.HasKey(x => x.PhotoPostId);

            builder.Property(x => x.UserId)
                .HasColumnName("user_id");

            builder.Property(x => x.ImageUrl)
                .HasColumnName("image_url")
                .IsRequired();

            builder.Property(x => x.AnnotationType)
                .HasConversion<string>();

            builder.Property(x => x.AnnotationValue);

            builder.Property(x => x.SentAt)
                .HasColumnType("timestamp")
                .IsRequired();

            builder.HasOne(u => u.User)
                .WithMany(p => p.PhotoPosts)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_PhotoPost_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
