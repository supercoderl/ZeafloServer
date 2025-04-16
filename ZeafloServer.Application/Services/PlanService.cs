using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.Queries.Plans.GetAll;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Plans;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Commands.Plans.CreatePlan;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Application.Services
{
    public class PlanService : IPlanService
    {
        private readonly IMediatorHandler _bus;

        public PlanService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<Guid> CreatePlanAsync(CreatePlanRequest request)
        {
            return await _bus.SendCommandAsync(new CreatePlanCommand(
                Guid.NewGuid(),
                request.Name,
                request.Description,
                request.MonthlyPrice,
                request.AnnualPrice,
                request.MaxSeats
            ));
        }

        public async Task<PageResult<PlanViewModel>> GetAllPlansAsync(PageQuery query, ActionStatus status, string searchTerm = "", SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllPlansQuery(query, status, searchTerm, sortQuery));
        }
    }
}
