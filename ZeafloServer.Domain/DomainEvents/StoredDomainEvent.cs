using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Events;

namespace ZeafloServer.Domain.DomainEvents
{
    public class StoredDomainEvent : DomainEvent
    {
        public Guid Id { get; private set; }
        public string Data { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string CorrelationId { get; private set; } = string.Empty;

        public StoredDomainEvent(
            DomainEvent domainEvent,
            string data,
            string email,
            string correlationId
        ) : base(domainEvent.AggregateId, domainEvent.MessageType)
        {
            Id = Guid.NewGuid();
            Data = data;
            Email = email;
            CorrelationId = correlationId;
        }

        //EF Constructor
        protected StoredDomainEvent() : base(Guid.NewGuid()) { }
    }
}
