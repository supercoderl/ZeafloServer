using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Extensions;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Places;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces.IRepositories;

namespace ZeafloServer.Application.Queries.Places.GetAll
{
    public sealed class GetAllPlacesQueryHandler : IRequestHandler<GetAllPlacesQuery, PageResult<PlaceViewModel>>
    {
        private readonly IPlaceRepository _placeRepository;
        private readonly ISortingExpressionProvider<PlaceViewModel, Place> _sortingExpressionProvider;

        public GetAllPlacesQueryHandler(
            IPlaceRepository placeRepository,
            ISortingExpressionProvider<PlaceViewModel, Place> sortingExpressionProvider
        )
        {
            _placeRepository = placeRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
        }

        public async Task<PageResult<PlaceViewModel>> Handle(GetAllPlacesQuery request, CancellationToken cancellationToken)
        {
            var query = _placeRepository.GetAllNoTracking().IgnoreQueryFilters();

            query = request.Status switch
            {
                ActionStatus.Deleted => query.Where(x => x.Deleted),
                ActionStatus.NotDeleted => query.Where(x => !x.Deleted),
                _ => query
            };

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                query = query.Where(x => x.Name.Contains(request.SearchTerm));

            if(request.Types.Count() > 0)
            {
                query = query.Where(x => request.Types.Contains(x.Type));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            query = query.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            query = query
                .OrderBy(x => x.CreatedAt)
                .Skip((request.Query.PageIndex - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize)
                .Include(x => x.PlaceLikes)
                .Include(x => x.City)
                .Include(x => x.PlaceImages);

            var places = await query.Select(place => PlaceViewModel.FromPlace(place)).ToListAsync();

            return new PageResult<PlaceViewModel>(
                totalCount,
                places,
                request.Query.PageIndex,
                request.Query.PageSize
            );
        }
    }
}
