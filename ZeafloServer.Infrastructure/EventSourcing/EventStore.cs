using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.DomainEvents;
using ZeafloServer.Domain.DomainNotifications;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Infrastructure.Database;
using ZeafloServer.Shared.Events;

namespace ZeafloServer.Infrastructure.EventSourcing
{
    public sealed class EventStore : IDomainEventStore
    {
        private readonly EventStoreDbContext _eventStoreDbContext;
        private readonly DomainNotificationStoreDbContext _domainNotificationStoreDbContext;
        private readonly IEventStoreContext _context;

        public EventStore(
            EventStoreDbContext eventStoreDbContext,
            DomainNotificationStoreDbContext domainNotificationStoreDbContext,
            IEventStoreContext context
        )
        {
            _eventStoreDbContext = eventStoreDbContext;
            _domainNotificationStoreDbContext = domainNotificationStoreDbContext;
            _context = context;
        }

        public async Task SaveAsync<T>(T domainEvent) where T : DomainEvent
        {
            var serializeData = JsonConvert.SerializeObject(domainEvent);

            switch (domainEvent)
            {
                case DomainNotification d:
                    var storeDomainNotification = new StoreDomainNotification(d, serializeData, _context.GetEmail(), _context.GetCorrelationId());
                    await _domainNotificationStoreDbContext.SaveChangesAsync();
                    break;
                default:
                    var storeDomainEvent = new StoredDomainEvent(domainEvent, serializeData, _context.GetEmail(), _context.GetCorrelationId());
                    _eventStoreDbContext.StoredDomainEvents.Add(storeDomainEvent);
                    await _eventStoreDbContext.SaveChangesAsync();
                    break;
            }
        }
    }
}
