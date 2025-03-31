using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events.Token
{
    public sealed class TokenUpdatedEvent : DomainEvent
    {
        public TokenUpdatedEvent(Guid tokenId) : base(tokenId)
        {
            
        }
    }
}
