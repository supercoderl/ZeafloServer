using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Notifications;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface INotificationsContext
    {
        Task<IEnumerable<NotificationViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
