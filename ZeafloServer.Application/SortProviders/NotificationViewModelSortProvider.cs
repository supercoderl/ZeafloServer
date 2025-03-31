using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Notifications;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.SortProviders
{
    public sealed class NotificationViewModelSortProvider : ISortingExpressionProvider<NotificationViewModel, Notification>
    {
        private static readonly Dictionary<string, Expression<Func<Notification, object>>> s_expressions = new()
        {
            { "created_at", notification => notification.CreatedAt }
        };

        public Dictionary<string, Expression<Func<Notification, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
