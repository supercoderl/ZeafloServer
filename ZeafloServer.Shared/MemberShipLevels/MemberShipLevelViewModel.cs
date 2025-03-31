using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Enums;

namespace ZeafloServer.Shared.MemberShipLevels
{
    public sealed record MemberShipLevelViewModel(
         Guid MemberShipLevelId,
         LevelType Type,
         int MinPoint
    );
}
