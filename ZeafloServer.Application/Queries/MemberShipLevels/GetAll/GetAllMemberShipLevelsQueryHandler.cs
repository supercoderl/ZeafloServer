using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Extensions;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.MemberShipLevels;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces.IRepositories;

namespace ZeafloServer.Application.Queries.MemberShipLevels.GetAll
{
    public sealed class GetAllMemberShipLevelsQueryHandler : IRequestHandler<GetAllMemberShipLevelsQuery, PageResult<MemberShipLevelViewModel>>
    {
        private readonly IMemberShipLevelRepository _memberShipLevelRepository;
        private readonly ISortingExpressionProvider<MemberShipLevelViewModel, MemberShipLevel> _sortingExpressionProvider;

        public GetAllMemberShipLevelsQueryHandler(
            IMemberShipLevelRepository memberShipLevelRepository,
            ISortingExpressionProvider<MemberShipLevelViewModel, MemberShipLevel> sortingExpressionProvider
        )
        {
            _memberShipLevelRepository = memberShipLevelRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
        }

        public async Task<PageResult<MemberShipLevelViewModel>> Handle(GetAllMemberShipLevelsQuery request, CancellationToken cancellationToken)
        {
            var query = _memberShipLevelRepository.GetAllNoTracking().IgnoreQueryFilters();

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

            var memberShipLevels = await query.Select(memberShipLevel => MemberShipLevelViewModel.FromMeberShipLevel(memberShipLevel)).ToListAsync();

            return new PageResult<MemberShipLevelViewModel>(
                totalCount,
                memberShipLevels,
                request.Query.PageIndex,
                request.Query.PageSize
            );
        }
    }
}
