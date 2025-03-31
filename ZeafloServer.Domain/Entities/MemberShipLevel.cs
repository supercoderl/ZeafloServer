using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Entities
{
    public class MemberShipLevel : Entity<Guid>
    {
        [Column("member_ship_level_id")]
        public Guid MemberShipLevelId { get; private set; }

        [Column("type")]
        public LevelType Type { get; private set; }

        [Column("min_point")]
        public int MinPoint { get; private set; }

        [InverseProperty("MemberShipLevel")]
        public virtual ICollection<UserLevel> UserLevels { get; set; } = new List<UserLevel>();

        public MemberShipLevel(
            Guid memberShipLevelId,
            LevelType type,
            int minPoint
        ) : base(memberShipLevelId )
        {
            MemberShipLevelId = memberShipLevelId;
            Type = type;
            MinPoint = minPoint;
        }

        public void SetType( LevelType type ) { Type = type; }
        public void SetMinPoint( int minPoint ) { MinPoint = minPoint; }
    }
}
