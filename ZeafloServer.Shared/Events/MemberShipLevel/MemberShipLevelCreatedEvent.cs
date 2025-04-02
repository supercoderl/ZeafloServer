using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events.MemberShipLevel
{
    public sealed class MemberShipLevelCreatedEvent : DomainEvent
    {
        public MemberShipLevelCreatedEvent(Guid memberShipLevelId) : base(memberShipLevelId)
        {
            
        }
    }
}
