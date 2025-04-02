using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events.City
{
    public sealed class CityCreatedEvent : DomainEvent
    {
        public CityCreatedEvent(Guid cityId) : base(cityId)
        {
            
        }
    }
}
