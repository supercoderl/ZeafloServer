using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Infrastructure.Database;

namespace ZeafloServer.Infrastructure.Repositories
{
    public sealed class MessageRepository : BaseRepository<Message, Guid>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Message>> GetBySenderAndReceiverAsync(Guid sender, Guid receiver)
        {
            return await DbSet.Where(x => x.SenderId == sender && x.ReceiverId == receiver && x.IsRead == false).ToListAsync();
        }
    }
}
