using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events.StoryActivity
{
    public sealed class StoryActivityCreatedEvent : DomainEvent
    {
        public StoryActivityCreatedEvent(Guid storyActivityId) : base(storyActivityId)
        {
            
        }
    }
}
