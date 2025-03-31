using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.DomainNotifications;

namespace ZeafloServer.Infrastructure.Configurations.EventSourcing
{
    public sealed class StoreDomainNotificationConfiguration : IEntityTypeConfiguration<StoreDomainNotification>
    {
        public void Configure(EntityTypeBuilder<StoreDomainNotification> builder)
        {
            builder.Property(c => c.MessageType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Key)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Value)
                .HasMaxLength(1024);

            builder.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.SerializedData)
                .IsRequired();

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.CorrelationId)
                .HasMaxLength(100)
                .HasColumnType("text");

            builder.Ignore(c => c.Data);

            builder.Property(c => c.SerializedData)
                .HasColumnName("Data");
        }
    }
}
