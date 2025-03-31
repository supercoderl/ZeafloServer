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
    public sealed class FriendShipConfiguration : IEntityTypeConfiguration<FriendShip>
    {
        public void Configure(EntityTypeBuilder<FriendShip> builder)
        {
            builder.HasKey(f => f.FriendShipId);

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(x => x.FriendId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.ToTable(tb => tb.HasCheckConstraint("CK_FriendShip_Status", "status IN ('Pending', 'Accepted', 'Blocked')"));

            builder.HasOne(u => u.User)
                .WithMany(f => f.FriendShips)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_FriendShip_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.Friend)
                .WithMany(f => f.Friends)
                .HasForeignKey(u => u.FriendId)
                .HasConstraintName("FK_FriendShip_User_FriendId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
