using MassTransit;
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
    public sealed class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(s => s.ScheduleId);

            builder.Property(s => s.UserId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(s => s.CityId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.TripDurationId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(s => s.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.Property(s => s.Note)
                .HasMaxLength(500);

            builder.HasOne(u => u.User)
                .WithMany(s => s.Schedules)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_Schedule_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.City)
                .WithMany(s => s.Schedules)
                .HasForeignKey(c => c.CityId)
                .HasConstraintName("FK_Schedule_City_CityId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(td => td.TripDuration)
                .WithMany(s => s.Schedules)
                .HasForeignKey(td => td.TripDurationId)
                .HasConstraintName("FK_Schedule_TripDuration_TripDurationId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
