using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Application.ViewModels.FriendShips
{
    public sealed record AddFriendRequest
    (
        Guid UserId,
        Guid FriendId
    );
}
