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
    public sealed class PostMediaConfiguration : IEntityTypeConfiguration<PostMedia>
    {
        public void Configure(EntityTypeBuilder<PostMedia> builder)
        {
            builder.HasKey(p => p.PostMediaId);

            builder.Property(p => p.PostId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(p => p.MediaUrl)
                .IsRequired();

            builder.Property(p => p.MediaType)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.ToTable(tb => tb.HasCheckConstraint("CK_PostMedia_MediaType", "media_type IN ('Image', 'Video', 'Gif', 'None')"));

            builder.HasOne(p => p.Post)
                .WithMany(pm => pm.PostMedias)
                .HasForeignKey(p => p.PostId)
                .HasConstraintName("FK_PostMedia_Post_PostId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
