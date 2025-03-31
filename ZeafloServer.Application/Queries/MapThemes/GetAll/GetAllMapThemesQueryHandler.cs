using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Extensions;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.MapThemes;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces.IRepositories;

namespace ZeafloServer.Application.Queries.MapThemes.GetAll
{
    public sealed class GetAllMapThemesQueryHandler : IRequestHandler<GetAllMapThemesQuery, PageResult<MapThemeViewModel>>
    {
        private readonly IMapThemeRepository _mapThemeRepository;
        private readonly ISortingExpressionProvider<MapThemeViewModel, MapTheme> _sortingExpressionProvider;

        public GetAllMapThemesQueryHandler(
            IMapThemeRepository mapThemeRepository,
            ISortingExpressionProvider<MapThemeViewModel, MapTheme> sortingExpressionProvider

        )
        {
            _mapThemeRepository = mapThemeRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
        }

        public async Task<PageResult<MapThemeViewModel>> Handle(GetAllMapThemesQuery request, CancellationToken cancellationToken)
        {
            var query = _mapThemeRepository.GetAllNoTracking().IgnoreQueryFilters();

            query = request.Status switch
            {
                ActionStatus.Deleted => query.Where(x => x.Deleted),
                ActionStatus.NotDeleted => query.Where(x => !x.Deleted),
                _ => query
            };

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                query = query.Where(x => 
                    (x.Name.Contains(request.SearchTerm)) ||
                    (x.Description != null && x.Description.Contains(request.SearchTerm)) ||
                    (x.MapStyle.Contains(request.SearchTerm))
               );

            var totalCount = await query.CountAsync(cancellationToken);

            query = query.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            query = query
                .Skip((request.Query.PageIndex - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize);

            var mapThemes = await query.Select(mapTheme => MapThemeViewModel.FromMapTheme(mapTheme)).ToListAsync();

            return new PageResult<MapThemeViewModel>(
                totalCount,
                mapThemes,
                request.Query.PageIndex,
                request.Query.PageSize
            );
        }
    }
}
