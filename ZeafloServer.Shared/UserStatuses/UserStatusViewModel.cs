using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.UserStatuses
{
    public sealed record UserStatusViewModel(
         Guid UserStatusId,
         Guid UserId,
         bool IsOnline,
         DateTime? LastSeen
    );
}
