using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Notifications;

namespace ZeafloServer.Domain.DomainNotifications
{
    public class StoreDomainNotification : DomainNotification
    {
        public Guid Id { get; private set; }
        public string SerializedData { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string CorrelationId { get; private set; } = string.Empty;

        public StoreDomainNotification(
            DomainNotification domainNotification,
            string data,
            string email,
            string correlationid
        ) : base(
            domainNotification.Key,
            domainNotification.Value,
            domainNotification.Code,
            null,
            domainNotification.AggregateId
        )
        {
            Id = Guid.NewGuid();
            Email = email;
            SerializedData = data;
            CorrelationId = correlationid;
            MessageType = domainNotification.MessageType;
        }

        //EF Constructor
        protected StoreDomainNotification() : base(string.Empty, string.Empty, string.Empty) { }
    }
}
