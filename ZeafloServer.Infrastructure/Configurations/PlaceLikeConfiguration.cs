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
    public sealed class PlaceLikeConfiguration : IEntityTypeConfiguration<PlaceLike>
    {
        public void Configure(EntityTypeBuilder<PlaceLike> builder)
        {
            builder.HasKey(p => p.PlaceLikeId);

            builder.Property(p => p.PlaceId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(p => p.UserId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.HasOne(u => u.User)
                .WithMany(pl => pl.PlaceLikes)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_PlaceLike_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Place)
                .WithMany(pl => pl.PlaceLikes)
                .HasForeignKey(p => p.PlaceId)
                .HasConstraintName("FK_PlaceLike_Place_PlaceId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
