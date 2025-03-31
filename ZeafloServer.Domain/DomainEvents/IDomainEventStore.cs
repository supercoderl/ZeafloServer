using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Events;

namespace ZeafloServer.Domain.DomainEvents
{
    public interface IDomainEventStore
    {
        Task SaveAsync<T>(T domainEvent) where T : DomainEvent;
    }
}
