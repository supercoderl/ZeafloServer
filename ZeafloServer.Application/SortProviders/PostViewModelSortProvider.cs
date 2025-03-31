using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Posts;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.SortProviders
{
    public sealed class PostViewModelSortProvider : ISortingExpressionProvider<PostViewModel, Post>
    {
        private static readonly Dictionary<string, Expression<Func<Post, object>>> s_expressions = new()
        {
            { "content", post => post.Content },
            { "created_at", post => post.CreatedAt },
        };

        public Dictionary<string, Expression<Func<Post, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
