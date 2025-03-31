using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Extensions;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Likes;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces.IRepositories;

namespace ZeafloServer.Application.Queries.Likes.GetAll
{
    public sealed class GetAllLikesQueryHandler : IRequestHandler<GetAllLikesQuery, PageResult<LikeViewModel>>
    {
        private readonly ILikeRepository _likeRepository;
        private readonly ISortingExpressionProvider<LikeViewModel, Like> _sortingExpressionProvider;

        public GetAllLikesQueryHandler(
            ILikeRepository likeRepository,
            ISortingExpressionProvider<LikeViewModel, Like> sortingExpressionProvider
        )
        {
            _likeRepository = likeRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
        }

        public async Task<PageResult<LikeViewModel>> Handle(GetAllLikesQuery request, CancellationToken cancellationToken)
        {
            var query = _likeRepository.GetAllNoTracking().IgnoreQueryFilters();

            query = request.Status switch
            {
                ActionStatus.Deleted => query.Where(x => x.Deleted),
                ActionStatus.NotDeleted => query.Where(x => !x.Deleted),
                _ => query
            };

            var totalCount = await query.CountAsync(cancellationToken);

            query = query.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            query = query
                .Skip((request.Query.PageIndex - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize);

            var likes = await query.Select(like => LikeViewModel.FromLike(like)).ToListAsync();

            return new PageResult<LikeViewModel>(
                totalCount,
                likes,
                request.Query.PageIndex,
                request.Query.PageSize
            );
        }
    }
}
