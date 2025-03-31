using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.DomainEvents;

namespace ZeafloServer.Infrastructure.Configurations.EventSourcing
{
    public sealed class StoreDomainEventConfiguration : IEntityTypeConfiguration<StoredDomainEvent>
    {
        public void Configure(EntityTypeBuilder<StoredDomainEvent> builder)
        {
            builder.Property(c => c.Timestamp)
                .HasColumnName("CreatedDate");

            builder.Property(c => c.MessageType)
                .HasColumnName("Action")
                .HasColumnType("varchar(100)");

            builder.Property(c => c.CorrelationId)
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .HasMaxLength(100)
                .HasColumnType("text");
        }
    }
}
