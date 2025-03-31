using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.Notifications;
using ZeafloServer.Shared.Enums;
using ZeafloServer.Shared.Notifications;
using NotificationType = ZeafloServer.Shared.Enums.NotificationType;

namespace ZeafloServer.Grpc.Contexts
{
    public class NotificationsContext : INotificationsContext
    {
        private readonly NotificationsApi.NotificationsApiClient _client;

        public NotificationsContext(NotificationsApi.NotificationsApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<NotificationViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetNotificationsByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.Notifications.Select(notification => new NotificationViewModel(
                Guid.Parse(notification.Id),
                Guid.Parse(notification.UserId),
                (NotificationType)notification.Type,
                Guid.Parse(notification.ReferenceId),
                notification.IsRead
            ));
        }
    }
}
