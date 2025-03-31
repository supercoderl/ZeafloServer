using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events.PostMedia
{
    public sealed class PostMediaCreatedEvent : DomainEvent
    {
        public PostMediaCreatedEvent(Guid postMediaId) : base(postMediaId)
        {
            
        }
    }
}
