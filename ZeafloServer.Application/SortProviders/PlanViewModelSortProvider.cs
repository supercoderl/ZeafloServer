using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Plans;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.SortProviders
{
    public sealed class PlanViewModelSortProvider : ISortingExpressionProvider<PlanViewModel, Plan>
    {
        private static readonly Dictionary<string, Expression<Func<Plan, object>>> s_expressions = new()
        {
            { "name", plan => plan.Name },
        };

        public Dictionary<string, Expression<Func<Plan, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
