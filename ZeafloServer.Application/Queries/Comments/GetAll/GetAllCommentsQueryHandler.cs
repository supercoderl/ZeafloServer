using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Extensions;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Comments;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces.IRepositories;

namespace ZeafloServer.Application.Queries.Comments.GetAll
{
    public sealed class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, PageResult<CommentViewModel>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ISortingExpressionProvider<CommentViewModel, Comment> _sortingExpressionProvider;

        public GetAllCommentsQueryHandler(
            ICommentRepository commentRepository,
            ISortingExpressionProvider<CommentViewModel, Comment> sortingExpressionProvider
        )
        {
            _commentRepository = commentRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
        }

        public async Task<PageResult<CommentViewModel>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
        {
            var query = _commentRepository.GetAllNoTracking().IgnoreQueryFilters();

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

            var comments = await query.Select(comment => CommentViewModel.FromComment(comment)).ToListAsync();

            return new PageResult<CommentViewModel>(
                totalCount,
                comments,
                request.Query.PageIndex,
                request.Query.PageSize
            );
        }
    }
}
