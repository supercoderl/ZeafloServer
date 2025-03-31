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
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(u => u.Email)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.Fullname)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(u => u.Bio);

            builder.Property(u => u.AvatarUrl)
                .IsRequired();

            builder.Property(u => u.CoverPhotoUrl)
                .IsRequired();

            builder.Property(u => u.PhoneNumber)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(u => u.Website);

            builder.Property(u => u.Location)
                .HasColumnType("varchar(100)");

            builder.Property(u => u.QrUrl)
                .IsRequired();

            builder.Property(u => u.BirthDate)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.Property(u => u.Gender)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnType("varchar(10)");

            builder.Property(u => u.IsOnline)
                .IsRequired()
                .HasColumnType("boolean");

            builder.Property(u => u.LastLoginTime)
                .HasColumnType("timestamp");

            builder.Property(u => u.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.Property(u => u.UpdatedAt)
                .HasColumnType("timestamp");

            builder.ToTable(tb => tb.HasCheckConstraint("CK_User_Gender", "gender IN ('Male', 'Female', 'Other')"));
        }
    }
}
