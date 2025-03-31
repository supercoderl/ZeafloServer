using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Extensions;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Notifications;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces.IRepositories;

namespace ZeafloServer.Application.Queries.Notifications.GetAll
{
    public sealed class GetAllNotificationsQueryHandler : IRequestHandler<GetAllNotificationsQuery, PageResult<NotificationViewModel>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ISortingExpressionProvider<NotificationViewModel, Notification> _sortingExpressionProvider;

        public GetAllNotificationsQueryHandler(
            INotificationRepository notificationRepository,
            ISortingExpressionProvider<NotificationViewModel, Notification> sortingExpressionProvider
        )
        {
            _notificationRepository = notificationRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
        }

        public async Task<PageResult<NotificationViewModel>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
        {
            var query = _notificationRepository.GetAllNoTracking().IgnoreQueryFilters();

            query = request.Status switch
            {
                ActionStatus.Deleted => query.Where(x => x.Deleted),
                ActionStatus.NotDeleted => query.Where(x => !x.Deleted),
                _ => query
            };

            var totalCount = await query.CountAsync(cancellationToken);

            query = query.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            query = query
                .Skip((request.Query.PageIndex - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize);

            var notifications = await query.Select(notification => NotificationViewModel.FromNotification(notification)).ToListAsync();

            return new PageResult<NotificationViewModel>(
                totalCount,
                notifications,
                request.Query.PageIndex,
                request.Query.PageSize
            );
        }
    }
}
