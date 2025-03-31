using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Messages;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.Queries.Messages.GetAll
{
    public sealed record GetAllMessagesQuery
    (
          PageQuery Query,
          ActionStatus Status,
          string SearchTerm = "",
          SortQuery? SortQuery = null
    ) : IRequest<PageResult<MessageViewModel>>;
}
