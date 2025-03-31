using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Enums;

namespace ZeafloServer.Shared.Notifications
{
    public sealed record NotificationViewModel(
         Guid NotificationId,
         Guid UserId,
         NotificationType Type,
         Guid ReferenceId,
         bool IsRead
    );
}
