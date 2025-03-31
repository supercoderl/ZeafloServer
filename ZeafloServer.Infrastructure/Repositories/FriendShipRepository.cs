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
    public sealed class FriendShipRepository : BaseRepository<FriendShip, Guid>, IFriendShipRepository
    {
        public FriendShipRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<FriendShip>> GetListByUserAsync(Guid userId)
        {
            return await DbSet
                .Where(x => (x.UserId == userId || x.FriendId == userId) 
                            && x.Status == Domain.Enums.FriendShipStatus.Accepted)
                .Include(x => x.Friend)
                    .ThenInclude(f => f.SenderMessages)
                .Include(x => x.Friend)
                    .ThenInclude(f => f.ReceiverMessages)
                .Include(x => x.User)
                    .ThenInclude(u => u.SenderMessages)
                .Include(x => x.User)
                    .ThenInclude(u => u.ReceiverMessages)
                .ToListAsync();
        }

        public async Task<FriendShip?> GetByUserAndFriendAsync(Guid userId, Guid friendId)
        {
            return await DbSet.SingleOrDefaultAsync(x =>
                (x.UserId == userId && x.FriendId == friendId) ||
                (x.UserId == friendId && x.FriendId == userId));
        }
    }
}
