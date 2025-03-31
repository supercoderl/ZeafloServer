using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events.FriendShip
{
    public sealed class FriendShipCreatedEvent : DomainEvent
    {
        public FriendShipCreatedEvent(Guid friendShipId) : base(friendShipId)
        {
            
        }
    }
}
