﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Comments;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.SortProviders
{
    public sealed class CommentViewModelSortProvider : ISortingExpressionProvider<CommentViewModel, Comment>
    {
        private static readonly Dictionary<string, Expression<Func<Comment, object>>> s_expressions = new()
        {
            { "content", comment => comment.Content },
            { "created_at", comment => comment.CreatedAt },
        };

        public Dictionary<string, Expression<Func<Comment, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
