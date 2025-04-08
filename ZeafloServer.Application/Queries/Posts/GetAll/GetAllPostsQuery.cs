using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Posts;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.Queries.Posts.GetAll
{
    public sealed record GetAllPostsQuery
    (
          PageQuery Query,
          ActionStatus Status,
          string Scope = "others",
          string SearchTerm = "",
          Guid? UserId = null,
          SortQuery? SortQuery = null
    ) : IRequest<PageResult<PostViewModel>>;
}
