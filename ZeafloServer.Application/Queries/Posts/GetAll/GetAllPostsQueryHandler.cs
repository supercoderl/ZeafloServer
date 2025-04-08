using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Extensions;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Posts;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;

namespace ZeafloServer.Application.Queries.Posts.GetAll
{
    public sealed class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, PageResult<PostViewModel>>
    {
        private readonly IPostRepository _postRepository;
        private readonly ISortingExpressionProvider<PostViewModel, Post> _sortingExpressionProvider;
        private readonly IUser _user;

        public GetAllPostsQueryHandler(
            IPostRepository postRepository,
            ISortingExpressionProvider<PostViewModel, Post> sortingExpressionProvider,
            IUser user
        )
        {
            _postRepository = postRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
            _user = user;
        }

        public async Task<PageResult<PostViewModel>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var query = _postRepository.GetAllNoTracking().IgnoreQueryFilters();

            query = request.Status switch
            {
                ActionStatus.Deleted => query.Where(x => x.Deleted),
                ActionStatus.NotDeleted => query.Where(x => !x.Deleted),
                _ => query
            };

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                query = query.Where(x => x.Content.Contains(request.SearchTerm)
               );

            query = QueryByScope(query, request.Scope, request.UserId);

            var totalCount = await query.CountAsync(cancellationToken);

            query = query.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            query = query
                .Skip((request.Query.PageIndex - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize)
                .Include(post => post.PostReactions)
                    .ThenInclude(u => u.User);

            var posts = await query
                .Select(post => PostViewModel.FromPost(
                    post,
                    new Author(
                        post.User != null ? post.User.Fullname : string.Empty,
                        post.User != null ? post.User.Location ?? string.Empty : string.Empty,
                        post.User != null ? post.User.AvatarUrl : string.Empty
                    )
                 ))
                .ToListAsync();

            return new PageResult<PostViewModel>(
                totalCount,
                posts,
                request.Query.PageIndex,
                request.Query.PageSize
            );
        }

        private IQueryable<Post> QueryByScope(IQueryable<Post> query, string scope = "others", Guid? userId = null)
        {
            return scope switch
            {
                "mine" => query.Where(x => x.UserId == _user.GetUserId()),
                "user" => query.Where(x => x.UserId == userId),
                _ => query.Where(x => x.UserId != _user.GetUserId())
            };
        }
    }
}
