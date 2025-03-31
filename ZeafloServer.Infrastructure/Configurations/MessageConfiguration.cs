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
    public sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.MessageId);

            builder.Property(m => m.SenderId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(m => m.ReceiverId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(m => m.Content)
                .IsRequired();

            builder.Property(m => m.MediaUrl);

            builder.Property(m => m.IsRead)
                .IsRequired()
                .HasColumnType("boolean");

            builder.Property(m => m.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.HasOne(u => u.Sender)
                .WithMany(m => m.SenderMessages)
                .HasForeignKey(u => u.SenderId)
                .HasConstraintName("FK_Message_User_SenderId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.Receiver)
                .WithMany(m => m.ReceiverMessages)
                .HasForeignKey(u => u.ReceiverId)
                .HasConstraintName("FK_Message_User_ReceiverId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
