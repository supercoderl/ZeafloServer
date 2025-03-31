using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.FriendShips;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.SortProviders
{
    public sealed class FriendShipViewModelSortProvider : ISortingExpressionProvider<FriendShipViewModel, FriendShip>
    {
        private static readonly Dictionary<string, Expression<Func<FriendShip, object>>> s_expressions = new()
        {
            { "created_at", friendShip => friendShip.CreatedAt }
        };

        public Dictionary<string, Expression<Func<FriendShip, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
