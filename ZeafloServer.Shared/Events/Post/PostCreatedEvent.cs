using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events.Post
{
    public sealed class PostCreatedEvent : DomainEvent
    {
        public PostCreatedEvent(Guid postId) : base(postId)
        {
            
        }
    }
}
