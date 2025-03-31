using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events
{
    public abstract class DomainEvent : MessageEvent, INotification
    {
        public DateTime Timestamp { get; private set; }
        protected DomainEvent(Guid aggregateId) : base(aggregateId)
        {
            Timestamp = DateTime.Now;
        }

        protected DomainEvent(Guid aggregateId, string? messageType) : base(aggregateId, messageType)
        {
            Timestamp = DateTime.Now;
        }
    }
}
