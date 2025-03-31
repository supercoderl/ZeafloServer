using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.Queries.Notifications.GetAll;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Notifications;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMediatorHandler _bus;

        public NotificationService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<PageResult<NotificationViewModel>> GetAllNotificationsAsync(PageQuery query, ActionStatus status, string searchTerm = "", SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllNotificationsQuery(query, status, searchTerm, sortQuery));   
        }
    }
}
