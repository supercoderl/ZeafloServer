using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events.Plan
{
    public sealed class PlanCreatedEvent : DomainEvent
    {
        public PlanCreatedEvent(Guid planId) : base(planId)
        {
            
        }
    }
}
