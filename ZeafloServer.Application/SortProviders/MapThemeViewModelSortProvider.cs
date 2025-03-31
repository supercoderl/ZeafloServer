using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.MapThemes;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.SortProviders
{
    public sealed class MapThemeViewModelSortProvider : ISortingExpressionProvider<MapThemeViewModel, MapTheme>
    {
        private static readonly Dictionary<string, Expression<Func<MapTheme, object>>> s_expressions = new()
        {
            { "name", mapTheme => mapTheme.Name },
            { "created_at", mapTheme => mapTheme.CreatedAt }
        };

        public Dictionary<string, Expression<Func<MapTheme, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
