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
    public sealed class TokenConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.HasKey(t => t.TokenId);

            builder.Property(t => t.AccessToken)
                .IsRequired();

            builder.Property(t => t.RefreshToken)
                .IsRequired();

            builder.Property(t => t.IsRefreshTokenRevoked)
                .IsRequired()
                .HasColumnType("boolean");

            builder.Property(t => t.RefreshTokenExpiredDate)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.HasOne(u => u.User)
                .WithMany(t => t.Tokens)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_Token_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
