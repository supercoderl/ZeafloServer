using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Domain.Interfaces.IRepositories
{
    public interface IMessageRepository : IRepository<Message, Guid>
    {
        Task<IEnumerable<Message>> GetBySenderAndReceiverAsync(Guid sender, Guid receiver);
    }
}
