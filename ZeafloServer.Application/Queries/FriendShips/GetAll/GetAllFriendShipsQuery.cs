using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.FriendShips;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.Queries.FriendShips.GetAll
{
    public sealed record GetAllFriendShipsQuery 
    (
          PageQuery Query,
          ActionStatus Status,
          Guid? UserId,
          string ActionType = "",
          string SearchTerm = "",
          SortQuery? SortQuery = null
    ) : IRequest<PageResult<FriendShipViewModel>>;
}
