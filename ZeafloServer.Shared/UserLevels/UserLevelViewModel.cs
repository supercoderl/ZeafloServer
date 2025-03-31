using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.UserLevels
{
    public sealed record UserLevelViewModel(
         Guid UserLevelId,
         Guid UserId,
         Guid MemberShipLevelId,
         int ZeafloPoint,
         DateTime AssignedAt
    );
}
