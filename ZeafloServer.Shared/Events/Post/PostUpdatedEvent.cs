using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events.Post
{
    public sealed class PostUpdatedEvent : DomainEvent
    {
        public PostUpdatedEvent(Guid postReactionId) : base(postReactionId)
        {
            
        }
    }
}
