using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events.PhotoPost
{
    public sealed class PhotoPostCreatedEvent : DomainEvent
    {
        public PhotoPostCreatedEvent(Guid photoPostId) : base(photoPostId)
        {
            
        }
    }
}
