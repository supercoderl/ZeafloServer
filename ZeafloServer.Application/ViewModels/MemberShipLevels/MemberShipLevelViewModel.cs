using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.ViewModels.MemberShipLevels
{
    public sealed class MemberShipLevelViewModel
    {
        public Guid MemberShipLevelId { get; set; }
        public LevelType Type { get; set; }
        public int MinPoint { get; set; }

        public static MemberShipLevelViewModel FromMeberShipLevel(MemberShipLevel memberShipLevel)
        {
            return new MemberShipLevelViewModel
            {
                MemberShipLevelId = memberShipLevel.MemberShipLevelId,
                Type = memberShipLevel.Type,
                MinPoint = memberShipLevel.MinPoint,
            };
        }
    }
}
