using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Application.ViewModels.Plans
{
    public sealed record CreatePlanRequest
    (
        string Name,
        string? Description,
        double MonthlyPrice,
        double AnnualPrice,
        int MaxSeats
    );
}
