using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Application.ViewModels.Plans;

namespace ZeafloServer.Application.Interfaces
{
    public interface IPlanService
    {
        public Task<PageResult<PlanViewModel>> GetAllPlansAsync(
            PageQuery query,
            ActionStatus status,
            string searchTerm = "",
            SortQuery? sortQuery = null
        );
        public Task<Guid> CreatePlanAsync(CreatePlanRequest request);
    }
}
