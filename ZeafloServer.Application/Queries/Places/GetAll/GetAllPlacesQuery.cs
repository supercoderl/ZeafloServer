using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Places;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.Queries.Places.GetAll
{
    public sealed record GetAllPlacesQuery
    (
          PageQuery Query,
          ActionStatus Status,
          List<PlaceType> Types,
          string SearchTerm = "",
          SortQuery? SortQuery = null
    ) : IRequest<PageResult<PlaceViewModel>>;
}
