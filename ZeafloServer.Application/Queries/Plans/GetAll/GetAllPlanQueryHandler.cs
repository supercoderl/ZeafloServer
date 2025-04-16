using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Application.ViewModels.Plans;
using ZeafloServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZeafloServer.Application.Extensions;

namespace ZeafloServer.Application.Queries.Plans.GetAll
{
    public sealed class GetAllPlansQueryHandler : IRequestHandler<GetAllPlansQuery, PageResult<PlanViewModel>>
    {
        private readonly IPlanRepository _planRepository;
        private readonly ISortingExpressionProvider<PlanViewModel, Plan> _sortingExpressionProvider;

        public GetAllPlansQueryHandler(
            IPlanRepository planRepository,
            ISortingExpressionProvider<PlanViewModel, Plan> sortingExpressionProvider
        )
        {
            _planRepository = planRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
        }

        public async Task<PageResult<PlanViewModel>> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
        {
            var query = _planRepository.GetAllNoTracking().IgnoreQueryFilters();

            query = request.Status switch
            {
                ActionStatus.Deleted => query.Where(x => x.Deleted),
                ActionStatus.NotDeleted => query.Where(x => !x.Deleted),
                _ => query
            };

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                query = query.Where(x => x.Name.Contains(request.SearchTerm));

            var totalCount = await query.CountAsync(cancellationToken);

            query = query.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            query = query
                .Skip((request.Query.PageIndex - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize);

            var plans = await query.Select(plan => PlanViewModel.FromPlan(plan)).ToListAsync();

            return new PageResult<PlanViewModel>(
                totalCount,
                plans,
                request.Query.PageIndex,
                request.Query.PageSize
            );
        }
    }
}
