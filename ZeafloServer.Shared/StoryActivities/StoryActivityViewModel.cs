using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Enums;

namespace ZeafloServer.Shared.StoryActivities
{
    public sealed record StoryActivityViewModel(
         Guid StoryActivityId,
         Guid UserId,
         ActionType ActionType,
         int PointEarned
    );
}
