using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Places;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.SortProviders
{
    public sealed class PlaceViewModelSortProvider : ISortingExpressionProvider<PlaceViewModel, Place>
    {
        private static readonly Dictionary<string, Expression<Func<Place, object>>> s_expressions = new()
        {
            { "name", place => place.Name },
            { "latitude", place => place.Latitude },
            { "longitude", place => place.Longitude },
            { "rating", place => place.Rating },
            { "review_count", place => place.ReviewCount },
            { "created_at", place => place.CreatedAt },
            { "updated_at", place => place.UpdatedAt },
        };

        public Dictionary<string, Expression<Func<Place, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
