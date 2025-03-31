using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Application.ViewModels.Likes;

namespace ZeafloServer.Application.Queries.Likes.GetAll
{
    public sealed record GetAllLikesQuery
    (
          PageQuery Query,
          ActionStatus Status,
          string SearchTerm = "",
          SortQuery? SortQuery = null
    ) : IRequest<PageResult<LikeViewModel>>;
}
