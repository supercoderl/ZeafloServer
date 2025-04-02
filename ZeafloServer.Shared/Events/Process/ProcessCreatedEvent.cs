using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events.Process
{
    public sealed class ProcessCreatedEvent : DomainEvent
    {
        public ProcessCreatedEvent(Guid processingId) : base(processingId)
        {
            
        }
    }
}
