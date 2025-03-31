using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Likes;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.SortProviders
{
    public sealed class LikeViewModelSortProvider : ISortingExpressionProvider<LikeViewModel, Like>
    {
        private static readonly Dictionary<string, Expression<Func<Like, object>>> s_expressions = new()
        {
            { "created_at", like => like.CreatedAt }
        };

        public Dictionary<string, Expression<Func<Like, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
