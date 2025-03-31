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
    public sealed class PlaceImageConfiguration : IEntityTypeConfiguration<PlaceImage>
    {
        public void Configure(EntityTypeBuilder<PlaceImage> builder)
        {
            builder.HasKey(p => p.PlaceImageId);

            builder.Property(p => p.PlaceId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(p => p.ImageUrl)
                .IsRequired();

            builder.HasOne(p => p.Place)
                .WithMany(pi => pi.PlaceImages)
                .HasForeignKey(p => p.PlaceId)
                .HasConstraintName("FK_PlaceImage_Place_PlaceId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
