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
    public sealed class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.HasKey(p => p.PlaceId);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(p => p.CityId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(p => p.Latitude)
                .IsRequired()
                .HasColumnType("double precision");

            builder.Property(p => p.Longitude)
                .IsRequired()
                .HasColumnType("double precision");

            builder.Property(p => p.Rating)
                .IsRequired()
                .HasColumnType("double precision");

            builder.Property(p => p.ReviewCount)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(p => p.IsOpen)
                .IsRequired()
                .HasColumnType("boolean");

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.Property(p => p.UpdatedAt)
                .HasColumnType("timestamp");

            builder.ToTable(tb => tb.HasCheckConstraint("CK_Place_Type", "type IN ('Restaurant', 'Hotel', 'HomeStay', 'Resort')"));

            builder.HasOne(c => c.City)
                .WithMany(p => p.Places)
                .HasForeignKey(c => c.CityId)
                .HasConstraintName("FK_Place_City_CityId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
