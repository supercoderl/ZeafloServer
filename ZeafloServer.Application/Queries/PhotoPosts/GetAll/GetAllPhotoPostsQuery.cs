using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Application.ViewModels.PhotoPosts;

namespace ZeafloServer.Application.Queries.PhotoPosts.GetAll
{
    public sealed record GetAllPhotoPostsQuery
    (
          PageQuery Query,
          ActionStatus Status,
          string Scope = "others",
          string SearchTerm = "",
          Guid? UserId = null,
          SortQuery? SortQuery = null
    ) : IRequest<PageResult<PhotoPostViewModel>>;
}
