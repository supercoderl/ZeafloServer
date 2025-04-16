using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Queries.Posts.GetAll;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Application.ViewModels.PhotoPosts;
using ZeafloServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZeafloServer.Application.Extensions;

namespace ZeafloServer.Application.Queries.PhotoPosts.GetAll
{
    public sealed class GetAllPhotoPostsQueryHandler : IRequestHandler<GetAllPhotoPostsQuery, PageResult<PhotoPostViewModel>>
    {
        private readonly IPhotoPostRepository _photoPostRepository;
        private readonly ISortingExpressionProvider<PhotoPostViewModel, PhotoPost> _sortingExpressionProvider;
        private readonly IUser _user;

        public GetAllPhotoPostsQueryHandler(
            IPhotoPostRepository photoPostRepository,
            ISortingExpressionProvider<PhotoPostViewModel, PhotoPost> sortingExpressionProvider,
            IUser user
        )
        {
            _photoPostRepository = photoPostRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
            _user = user;
        }

        public async Task<PageResult<PhotoPostViewModel>> Handle(GetAllPhotoPostsQuery request, CancellationToken cancellationToken)
        {
            var query = _photoPostRepository.GetAllNoTracking().IgnoreQueryFilters();

            query = request.Status switch
            {
                ActionStatus.Deleted => query.Where(x => x.Deleted),
                ActionStatus.NotDeleted => query.Where(x => !x.Deleted),
                _ => query
            };

            /*  if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                query = query.Where(x => x.Content.Contains(request.SearchTerm)
            );*/

            query = QueryByScope(query, request.Scope, request.UserId);

            var totalCount = await query.CountAsync(cancellationToken);

            query = query.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            query = query
                .Skip((request.Query.PageIndex - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize)
                .Include(post => post.User)
                .OrderByDescending(post => post.SentAt);

            var posts = await query
                .Select(post => PhotoPostViewModel.FromPhotoPost(post))
                .ToListAsync();

            return new PageResult<PhotoPostViewModel>(
                totalCount,
                posts,
                request.Query.PageIndex,
                request.Query.PageSize
            );
        }

        private IQueryable<PhotoPost> QueryByScope(IQueryable<PhotoPost> query, string scope = "others", Guid? userId = null)
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
