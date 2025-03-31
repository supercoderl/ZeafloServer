using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels.Users;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.Queries.Users.GetAll
{
    public sealed record GetAllUsersQuery
    (
          PageQuery Query,
          ActionStatus Status,
          string SearchTerm = "",
          SortQuery? SortQuery = null
    ) : IRequest<PageResult<UserViewModel>>;
}
