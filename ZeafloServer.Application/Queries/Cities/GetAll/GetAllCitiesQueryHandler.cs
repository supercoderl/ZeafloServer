using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Extensions;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Cities;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces.IRepositories;

namespace ZeafloServer.Application.Queries.Cities.GetAll
{
    public sealed class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, PageResult<CityViewModel>>
    {
        private readonly ICityRepository _cityRepository;
        private readonly ISortingExpressionProvider<CityViewModel, City> _sortingExpressionProvider;

        public GetAllCitiesQueryHandler(
            ICityRepository cityRepository,
            ISortingExpressionProvider<CityViewModel, City> sortingExpressionProvider
        )
        {
            _cityRepository = cityRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
        }

        public async Task<PageResult<CityViewModel>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var query = _cityRepository.GetAllNoTracking().IgnoreQueryFilters();

            query = request.Status switch
            {
                ActionStatus.Deleted => query.Where(x => x.Deleted),
                ActionStatus.NotDeleted => query.Where(x => !x.Deleted),
                _ => query
            };

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                query = query.Where(x =>
                    (x.Name.Contains(request.SearchTerm)) ||
                    (x.PostalCode.Contains(request.SearchTerm)
               ));

            var totalCount = await query.CountAsync(cancellationToken);

            query = query.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            query = query
                .Skip((request.Query.PageIndex - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize);

            var cities = await query.Select(city => CityViewModel.FromCity(city)).ToListAsync();

            return new PageResult<CityViewModel>(
                totalCount,
                cities,
                request.Query.PageIndex,
                request.Query.PageSize
            );
        }
    }
}
