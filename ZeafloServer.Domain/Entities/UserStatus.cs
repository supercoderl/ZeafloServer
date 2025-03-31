using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Entities
{
    public class UserStatus : Entity<Guid>
    {
        [Column("user_status_id")]
        public Guid UserStatusId { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("is_online")]
        public bool IsOnline { get; private set; }

        [Column("last_seen")]
        public DateTime? LastSeen { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("UserStatuses")]
        public virtual User? User { get; set; }

        public UserStatus(
            Guid userStatusId,
            Guid userId,
            bool isOnline,
            DateTime? lastSeen
        ) : base(userStatusId)
        {
            UserStatusId = userStatusId;
            UserId = userId;
            IsOnline = isOnline;
            LastSeen = lastSeen;
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetIsOnline( bool isOnline ) { IsOnline = isOnline; }
        public void SetLastSeen( DateTime? lastSeen ) { LastSeen = lastSeen; }
    }
}
