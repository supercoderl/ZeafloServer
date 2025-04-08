using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels.TripDurations;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.SortProviders
{
    public sealed class TripDurationViewModelSortProvider : ISortingExpressionProvider<TripDurationViewModel, TripDuration>
    {
        private static readonly Dictionary<string, Expression<Func<TripDuration, object>>> s_expressions = new()
        {
            { "label", tripDuration => tripDuration.Label },
            { "days", tripDuration => tripDuration.Days },
            { "nights", tripDuration => tripDuration.Nights }
        };

        public Dictionary<string, Expression<Func<TripDuration, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
