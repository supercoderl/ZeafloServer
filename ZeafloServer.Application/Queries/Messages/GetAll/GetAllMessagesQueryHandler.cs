using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Extensions;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Messages;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces.IRepositories;

namespace ZeafloServer.Application.Queries.Messages.GetAll
{
    public sealed class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, PageResult<MessageViewModel>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ISortingExpressionProvider<MessageViewModel, Message> _sortingExpressionProvider;

        public GetAllMessagesQueryHandler(
            IMessageRepository messageRepository,
            ISortingExpressionProvider<MessageViewModel, Message> sortingExpressionProvider
        )
        {
            _messageRepository = messageRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
        }

        public async Task<PageResult<MessageViewModel>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
        {
            var query = _messageRepository.GetAllNoTracking().IgnoreQueryFilters();

            query = request.Status switch
            {
                ActionStatus.Deleted => query.Where(x => x.Deleted),
                ActionStatus.NotDeleted => query.Where(x => !x.Deleted),
                _ => query
            };

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                query = query.Where(x => x.Content.Contains(request.SearchTerm)
               );

            var totalCount = await query.CountAsync(cancellationToken);

            query = query.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            query = query
                .Skip((request.Query.PageIndex - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize);

            var messages = await query.Select(message => MessageViewModel.FromMessage(message)).ToListAsync();

            return new PageResult<MessageViewModel>(
                totalCount,
                messages,
                request.Query.PageIndex,
                request.Query.PageSize
            );
        }
    }
}
