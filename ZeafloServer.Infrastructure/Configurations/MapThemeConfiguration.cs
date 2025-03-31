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
    public sealed class MapThemeConfiguration : IEntityTypeConfiguration<MapTheme>
    {
        public void Configure(EntityTypeBuilder<MapTheme> builder)
        {
            builder.HasKey(m => m.MapThemeId);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(m => m.Description);

            builder.Property(m => m.MapStyle)
                .IsRequired();

            builder.Property(m => m.PreviewUrl)
                .IsRequired();

            builder.Property(m => m.IsPremium)
                .IsRequired()
                .HasColumnType("boolean");

            builder.Property(m => m.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp");
        }
    }
}
