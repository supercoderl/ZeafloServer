using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.ViewModels.UserLevels
{
    public sealed class UserLevelViewModel
    {
        public Guid UserLevelId { get; set; }
        public Guid UserId { get; set; }
        public Guid MemberShipLevelId { get; set; }
        public int ZeafloPoint {  get; set; }
        public DateTime AssignedAt { get; set; }
        public LevelType Type { get; set; }

        public static UserLevelViewModel FromUserLevel(UserLevel userLevel)
        {
            return new UserLevelViewModel
            {
                UserLevelId = userLevel.UserLevelId,
                UserId = userLevel.UserId,
                MemberShipLevelId = userLevel.MemberShipLevelId,
                ZeafloPoint = userLevel.ZeafloPoint,
                AssignedAt = userLevel.AssignedAt,
                Type = userLevel.MemberShipLevel?.Type ?? LevelType.Silver
            };
        }
    }
}
