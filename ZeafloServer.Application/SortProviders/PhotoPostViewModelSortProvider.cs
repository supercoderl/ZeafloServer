using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.PhotoPosts;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.SortProviders
{
    public sealed class PhotoPostViewModelSortProvider : ISortingExpressionProvider<PhotoPostViewModel, PhotoPost>
    {
        private static readonly Dictionary<string, Expression<Func<PhotoPost, object>>> s_expressions = new()
        {
            
        };

        public Dictionary<string, Expression<Func<PhotoPost, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
