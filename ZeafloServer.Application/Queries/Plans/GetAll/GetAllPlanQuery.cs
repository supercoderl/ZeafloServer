using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Application.ViewModels.Plans;

namespace ZeafloServer.Application.Queries.Plans.GetAll
{
    public sealed record GetAllPlansQuery
    (
          PageQuery Query,
          ActionStatus Status,
          string SearchTerm = "",
          SortQuery? SortQuery = null
    ) : IRequest<PageResult<PlanViewModel>>;
}
