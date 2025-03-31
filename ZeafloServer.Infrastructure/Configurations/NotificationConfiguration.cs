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
    public sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(n => n.NotificationId);

            builder.Property(n => n.UserId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(n => n.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(n => n.ReferenceId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(n => n.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.Property(n => n.IsRead)
                .IsRequired()
                .HasColumnType("boolean");

            builder.ToTable(tb => tb.HasCheckConstraint("CK_Notification_Type", "type IN ('Like', 'Comment', 'FriendRequest', 'Message')"));

            builder.HasOne(u => u.User)
                .WithMany(n => n.Notifications)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_Notification_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
