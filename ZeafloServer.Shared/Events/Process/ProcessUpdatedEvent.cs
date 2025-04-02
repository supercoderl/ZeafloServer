using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events.Process
{
    public sealed class ProcessUpdatedEvent : DomainEvent
    {
        public ProcessUpdatedEvent(Guid processingId) : base(processingId)
        {
            
        }
    }
}
