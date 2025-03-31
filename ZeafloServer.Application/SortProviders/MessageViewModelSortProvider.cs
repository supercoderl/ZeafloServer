using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Messages;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.SortProviders
{
    public sealed class MessageViewModelSortProvider : ISortingExpressionProvider<MessageViewModel, Message>
    {
        private static readonly Dictionary<string, Expression<Func<Message, object>>> s_expressions = new()
        {
            { "content", message => message.Content },
            { "created_at", message => message.CreatedAt },
        };

        public Dictionary<string, Expression<Func<Message, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
