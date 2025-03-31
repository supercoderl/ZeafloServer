using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Cities;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.SortProviders
{
    public sealed class CityViewModelSortProvider : ISortingExpressionProvider<CityViewModel, City>
    {
        private static readonly Dictionary<string, Expression<Func<City, object>>> s_expressions = new()
        {
            { "name", city => city.Name },
            { "postalCode", city => city.PostalCode },
            { "latitude", city => city.Latitude },
            { "longitude", city => city.Longitude }
        };

        public Dictionary<string, Expression<Func<City, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
