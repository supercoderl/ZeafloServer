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
    public sealed class TripDurationConfiguration : IEntityTypeConfiguration<TripDuration>
    {
        public void Configure(EntityTypeBuilder<TripDuration> builder)
        {
            builder.HasKey(td => td.TripDurationId);

            builder.Property(td => td.Label)
                .IsRequired();

            builder.Property(td => td.Days)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(td => td.Nights)
                .IsRequired()
                .HasColumnType("int");
        }
    }
}
