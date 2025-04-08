using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events.TripDuration
{
    public sealed class TripDurationCreatedEvent : DomainEvent
    {
        public TripDurationCreatedEvent(Guid tripDurationId) : base(tripDurationId)
        {
            
        }
    }
}
