using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Entities
{
    public class FriendShip : Entity<Guid>
    {
        [Column("friend_ship_id")]
        public Guid FriendShipId { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("friend_id")]
        public Guid FriendId { get; private set; }

        [Column("status")]
        public FriendShipStatus Status { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("FriendShips")]
        public virtual User? User { get; set; }

        [ForeignKey("FriendId")]
        [InverseProperty("Friends")]
        public virtual User? Friend { get; set; }

        public FriendShip(
            Guid friendShipId,
            Guid userId,
            Guid friendId,
            FriendShipStatus status,
            DateTime createdAt
        ) : base(friendShipId)
        {
            FriendShipId = friendShipId;
            UserId = userId;
            FriendId = friendId;
            Status = status;
            CreatedAt = createdAt;
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetFriendId( Guid friendId ) { FriendId = friendId; }
        public void SetFriendShipStatus( FriendShipStatus status ) { Status = status; }
        public void SetCreatedAt( DateTime createdAt ) { CreatedAt = createdAt; }
    }
}
