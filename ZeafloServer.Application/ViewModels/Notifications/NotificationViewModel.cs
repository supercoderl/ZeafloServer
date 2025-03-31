using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.ViewModels.Notifications
{
    public sealed class NotificationViewModel
    {
        public Guid NotificationId { get; set; }
        public Guid UserId { get; set; }
        public NotificationType Type { get; set; }
        public Guid ReferenceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }

        public static NotificationViewModel FromNotification(Notification notification)
        {
            return new NotificationViewModel
            {
                NotificationId = notification.NotificationId,
                UserId = notification.UserId,
                Type = notification.Type,
                ReferenceId = notification.ReferenceId,
                CreatedAt = notification.CreatedAt,
                IsRead = notification.IsRead,
            };
        }
    }
}
