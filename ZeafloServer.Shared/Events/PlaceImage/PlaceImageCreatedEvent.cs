using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events.PlaceImage
{
    public sealed class PlaceImageCreatedEvent : DomainEvent
    {
        public PlaceImageCreatedEvent(Guid placeImageId) : base(placeImageId)
        {
            
        }
    }
}
