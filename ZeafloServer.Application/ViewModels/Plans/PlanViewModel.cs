using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.ViewModels.Plans
{
    public sealed class PlanViewModel
    {
        public Guid PlanId { get; set; }    
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }    
        public double MonthlyPrice { get; set; }
        public double AnnualPrice { get; set; }
        public int MaxSeats { get; set; }

        public static PlanViewModel FromPlan(Plan plan)
        {
            return new PlanViewModel
            {
                PlanId = plan.PlanId,
                Name = plan.Name,
                Description = plan.Description,
                MonthlyPrice = plan.MonthlyPrice,
                AnnualPrice = plan.AnnualPrice,
                MaxSeats = plan.MaxSeats,
            };
        }
    }
}
