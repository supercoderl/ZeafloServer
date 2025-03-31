
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Comments;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.Queries.Comments.GetAll
{
    public sealed record GetAllCommentsQuery
    (
          PageQuery Query,
          ActionStatus Status,
          string SearchTerm = "",
          SortQuery? SortQuery = null
    ) : IRequest<PageResult<CommentViewModel>>;
}
