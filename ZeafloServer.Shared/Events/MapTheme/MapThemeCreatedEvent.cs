using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events.MapTheme
{
    public sealed class MapThemeCreatedEvent : DomainEvent
    {
        public MapThemeCreatedEvent(Guid mapThemeId) : base(mapThemeId)
        {
            
        }
    }
}
