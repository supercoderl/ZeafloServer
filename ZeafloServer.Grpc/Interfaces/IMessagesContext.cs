using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Messages;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface IMessagesContext
    {
        Task<IEnumerable<MessageViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
