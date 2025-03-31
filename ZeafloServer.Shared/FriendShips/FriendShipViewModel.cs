using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Enums;

namespace ZeafloServer.Shared.FriendShips
{
    public sealed record FriendShipViewModel(
         Guid FriendShipId,
         Guid UserId,
         Guid FriendId,
         FriendShipStatus Status
    );
}
