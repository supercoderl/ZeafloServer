using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events.Post
{
    public sealed class PostReactedEvent : DomainEvent
    {
        public PostReactedEvent(Guid postReactionId) : base(postReactionId)
        {
            
        }
    }
}
