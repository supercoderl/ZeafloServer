using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Entities
{
    public class UserLevel : Entity<Guid>
    {
        [Column("user_level_id")]
        public Guid UserLevelId { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("member_ship_level_id")]
        public Guid MemberShipLevelId { get; private set; }

        [Column("zeaflo_point")]
        public int ZeafloPoint {  get; private set; }

        [Column("assigned_at")]
        public DateTime AssignedAt { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("UserLevel")]
        public virtual User? User { get; set; }

        [ForeignKey("MemberShipLevelId")]
        [InverseProperty("UserLevels")]
        public virtual MemberShipLevel? MemberShipLevel { get; set; }

        public UserLevel(
            Guid userLevelId,
            Guid userId,
            Guid memberShipLevelId,
            int zeafloPoint,
            DateTime assignedAt
        ) : base(userLevelId)
        {
            UserLevelId = userLevelId;
            UserId = userId;
            MemberShipLevelId = memberShipLevelId;
            ZeafloPoint = zeafloPoint;
            AssignedAt = assignedAt;
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetMemberShipLevelId(Guid memberShipLevelId) { MemberShipLevelId= memberShipLevelId; }
        public void SetZeafloPoint( int zeafloPoint ) { ZeafloPoint = zeafloPoint; }
        public void SetAssignedAt( DateTime assignedAt ) { AssignedAt = assignedAt; }
    }
}
