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
    public sealed class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.PostId);

            builder.Property(p => p.UserId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(p => p.Title)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Content)
                .IsRequired();

            builder.Property(p => p.ThumbnailUrl);

            builder.Property(p => p.Visibility)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.ToTable(tb => tb.HasCheckConstraint("CK_Post_Visibility", "visibility IN ('Public', 'Friends', 'Private')"));

            builder.HasOne(u => u.User)
                .WithMany(p => p.Posts)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_Post_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
