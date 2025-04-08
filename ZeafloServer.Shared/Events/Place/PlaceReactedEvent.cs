using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events.Place
{
    public sealed class PlaceReactedEvent : DomainEvent
    {
        public PlaceReactedEvent(Guid placeLikeId) : base(placeLikeId)
        {
            
        }
    }
}
