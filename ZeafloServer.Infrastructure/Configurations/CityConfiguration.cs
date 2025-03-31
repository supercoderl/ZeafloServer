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
    public sealed class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.CityId);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.PostalCode)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Latitude)
                .IsRequired()
                .HasColumnType("double precision");

            builder.Property(c => c.Longitude)
                .IsRequired()
                .HasColumnType("double precision");
        }
    }
}
