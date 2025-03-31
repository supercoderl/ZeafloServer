using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using MediatR;
using ZeafloServer.Application.ViewModels.FriendShips;

namespace ZeafloServer.Application.Queries.FriendShips.GetContacts
{
    public sealed record GetContactsQuery
    (
          PageQuery Query,
          ActionStatus Status,
          Guid UserId,
          string SearchTerm = ""
    ) : IRequest<PageResult<ContactInfo>>;
}
