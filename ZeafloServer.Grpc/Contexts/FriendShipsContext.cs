using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.FriendShips;
using ZeafloServer.Shared.Enums;
using ZeafloServer.Shared.FriendShips;
using FriendShipStatus = ZeafloServer.Shared.Enums.FriendShipStatus;

namespace ZeafloServer.Grpc.Contexts
{
    public class FriendShipsContext : IFriendShipsContext
    {
        private readonly FriendShipsApi.FriendShipsApiClient _client;

        public FriendShipsContext(FriendShipsApi.FriendShipsApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<FriendShipViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetFriendShipsByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.FriendShips.Select(friendShip => new FriendShipViewModel(
                Guid.Parse(friendShip.Id),
                Guid.Parse(friendShip.UserId),
                Guid.Parse(friendShip.FriendId),
                (FriendShipStatus)friendShip.Status
            ));
        }
    }
}
