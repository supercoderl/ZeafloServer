using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Entities
{
    public class Notification : Entity<Guid>
    {
        [Column("notification_id")]
        public Guid NotificationId { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("type")]
        public NotificationType Type { get; private set; }

        [Column("reference_id")]
        public Guid ReferenceId { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }

        [Column("is_read")]
        public bool IsRead { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("Notifications")]
        public virtual User? User { get; set; }

        public Notification(
            Guid notificationId,
            Guid userId,
            NotificationType type,
            Guid referenceId,
            DateTime createdAt,
            bool isRead
        ) : base(notificationId )
        {
            NotificationId = notificationId;
            UserId = userId;
            Type = type;
            ReferenceId = referenceId;
            CreatedAt = createdAt;
            IsRead = isRead;
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetType( NotificationType type ) { Type = type; }
        public void SetReferenceId( Guid referenceId ) { ReferenceId = referenceId; }
        public void SetCreatedAt( DateTime createdAt ) { CreatedAt = createdAt; }
        public void SetIsRead( bool isRead ) { IsRead = isRead; }
    }
}
