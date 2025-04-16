using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Enums;

namespace ZeafloServer.Shared.Events
{
    public class GainPointMessageEvent
    {
        public Guid UserId { get; }
        public ActionType ActionType { get; }

        public GainPointMessageEvent(
            Guid userId,
            ActionType actionType
        )
        {
            UserId = userId;
            ActionType = actionType;
        }
    }
}
