using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.FriendShips;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface IFriendShipsContext
    {
        Task<IEnumerable<FriendShipViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
