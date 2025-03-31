using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Extensions;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.FriendShips;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces.IRepositories;

namespace ZeafloServer.Application.Queries.FriendShips.GetAll
{
    public sealed class GetAllFriendShipsQueryHandler : IRequestHandler<GetAllFriendShipsQuery, PageResult<FriendShipViewModel>>
    {
        private readonly IFriendShipRepository _friendShipRepository;
        private readonly ISortingExpressionProvider<FriendShipViewModel, FriendShip> _sortingExpressionProvider;

        public GetAllFriendShipsQueryHandler(
            IFriendShipRepository friendShipRepository,
            ISortingExpressionProvider<FriendShipViewModel, FriendShip> sortingExpressionProvider
        )
        {
            _friendShipRepository = friendShipRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
        }

        public async Task<PageResult<FriendShipViewModel>> Handle(GetAllFriendShipsQuery request, CancellationToken cancellationToken)
        {
            var query = _friendShipRepository.GetAllNoTracking().IgnoreQueryFilters();

            query = request.Status switch
            {
                ActionStatus.Deleted => query.Where(x => x.Deleted),
                ActionStatus.NotDeleted => query.Where(x => !x.Deleted),
                _ => query
            };

            if(request.UserId != null)
            {
                query = request.ActionType switch
                {
                    "SentRequests" => query.Where(x => x.UserId == request.UserId && x.Status == FriendShipStatus.Pending),
                    "ReceivedRequests" => query.Where(x => x.FriendId == request.UserId && x.Status == FriendShipStatus.Pending),
                    _ => query.Where(x => (x.UserId == request.UserId || x.FriendId == request.UserId) && x.Status == FriendShipStatus.Accepted)
                };
            }

            var totalCount = await query.CountAsync(cancellationToken);

            query = query.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            query = query
                .Skip((request.Query.PageIndex - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize);

            var friendShips = await query.Select(friendShip =>
                FriendShipViewModel.FromFriendShip(
                    friendShip,
                    friendShip.UserId == request.UserId ? friendShip.UserId : friendShip.FriendId,
                    new ContactInfo
                    {
                        FriendId = friendShip.UserId == request.UserId ? friendShip.FriendId : friendShip.UserId,
                        Username = friendShip.UserId == request.UserId
                            ? (friendShip.Friend != null ? friendShip.Friend.Username : string.Empty)
                            : (friendShip.User != null ? friendShip.User.Username : string.Empty),
                        Fullname = friendShip.UserId == request.UserId
                            ? (friendShip.Friend != null ? friendShip.Friend.Fullname : string.Empty)
                            : (friendShip.User != null ? friendShip.User.Fullname : string.Empty),
                        AvatarUrl = friendShip.UserId == request.UserId
                            ? (friendShip.Friend != null ? friendShip.Friend.AvatarUrl : string.Empty)
                            : (friendShip.User != null ? friendShip.User.AvatarUrl : string.Empty),
                        Status = friendShip.Status
                    },
                    null
                )
            ).ToListAsync();

            return new PageResult<FriendShipViewModel>(
                totalCount,
                friendShips,
                request.Query.PageIndex,
                request.Query.PageSize
            );
        }
    }
}
