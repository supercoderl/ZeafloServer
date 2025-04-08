using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Queries.Cities.GetAll;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Application.ViewModels.TripDurations;
using ZeafloServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZeafloServer.Application.Extensions;

namespace ZeafloServer.Application.Queries.TripDurations.GetAll
{
    public sealed class GetAllTripDurationsQueryHandler : IRequestHandler<GetAllTripDurationsQuery, PageResult<TripDurationViewModel>>
    {
        private readonly ITripDurationRepository _tripDurationRepository;
        private readonly ISortingExpressionProvider<TripDurationViewModel, TripDuration> _sortingExpressionProvider;

        public GetAllTripDurationsQueryHandler(
            ITripDurationRepository tripDurationRepository,
            ISortingExpressionProvider<TripDurationViewModel, TripDuration> sortingExpressionProvider
        )
        {
            _tripDurationRepository = tripDurationRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
        }

        public async Task<PageResult<TripDurationViewModel>> Handle(GetAllTripDurationsQuery request, CancellationToken cancellationToken)
        {
            var query = _tripDurationRepository.GetAllNoTracking().IgnoreQueryFilters();

            query = request.Status switch
            {
                ActionStatus.Deleted => query.Where(x => x.Deleted),
                ActionStatus.NotDeleted => query.Where(x => !x.Deleted),
                _ => query
            };

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                query = query.Where(x => x.Label.Contains(request.SearchTerm));

            var totalCount = await query.CountAsync(cancellationToken);

            query = query.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            query = query
                .Skip((request.Query.PageIndex - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize);

            var tripDurations = await query.Select(tripDuration => TripDurationViewModel.FromTripDuration(tripDuration)).ToListAsync();

            return new PageResult<TripDurationViewModel>(
                totalCount,
                tripDurations,
                request.Query.PageIndex,
                request.Query.PageSize
            );
        }
    }
}
