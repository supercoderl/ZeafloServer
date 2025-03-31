using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Infrastructure.EventSourcing
{
    public interface IEventStoreContext
    {
        public string GetEmail();
        public string GetCorrelationId();
    }
}
