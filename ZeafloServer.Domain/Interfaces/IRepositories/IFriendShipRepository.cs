using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Domain.Interfaces.IRepositories
{
    public interface IFriendShipRepository : IRepository<FriendShip, Guid>
    {
        Task<FriendShip?> GetByUserAndFriendAsync(Guid userId, Guid friendId);
        Task<IEnumerable<FriendShip>> GetListByUserAsync(Guid userId);
    }
}
