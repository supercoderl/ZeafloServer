﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Sorting;

namespace ZeafloServer.Application.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> GetOrderedQueryable<TEntity, TViewModel>(
            this IQueryable<TEntity> query,
            SortQuery? sort,
            ISortingExpressionProvider<TViewModel, TEntity> expressionProvider
        )
        {
            return GetOrderedQueryable(query, sort, expressionProvider.GetSortingExpressions());
        }

        public static IQueryable<TEntity> GetOrderedQueryable<TEntity>(
            this IQueryable<TEntity> query,
            SortQuery? sort,
            Dictionary<string, Expression<Func<TEntity, object>>> fieldExpression
        )
        {
            if (sort is null || !sort.Parameters.Any())
            {
                return query;
            }

            var sorted = GetFirstOrderLevelQuery(query, sort.Parameters.First(), fieldExpression);

            for (var i = 1; i < sort.Parameters.Count; i++)
            {
                sorted = GetMultiLevelOrderedQuery(sorted, sort.Parameters[i], fieldExpression);
            }

            return sorted;
        }

        private static IOrderedQueryable<TEntity> GetFirstOrderLevelQuery<TEntity>(
            IQueryable<TEntity> query,
            SortParameter param,
            Dictionary<string, Expression<Func<TEntity, object>>> fieldExpressions
        )
        {
            if (!fieldExpressions.TryGetValue(param.ParameterName, out var fieldExpression))
            {
                throw new Exception($"{param.ParameterName} is not a sortable field.");
            }

            return param.Order switch
            {
                SortOrder.Ascending => query.OrderBy(fieldExpression),
                SortOrder.Descending => query.OrderByDescending(fieldExpression),
                _ => throw new InvalidOperationException($"{param.Order} is not a supported value.")
            };
        }

        private static IOrderedQueryable<TEntity> GetMultiLevelOrderedQuery<TEntity>(
            IOrderedQueryable<TEntity> query,
            SortParameter param,
            Dictionary<string, Expression<Func<TEntity, object>>> fieldExpressions
        )
        {
            if (!fieldExpressions.TryGetValue(param.ParameterName, out var fieldExpression))
            {
                throw new Exception($"{param.ParameterName} is not a sortable field.");
            }

            return param.Order switch
            {
                SortOrder.Ascending => query.ThenBy(fieldExpression),
                SortOrder.Descending => query.ThenByDescending(fieldExpression),
                _ => throw new InvalidOperationException($"{param.Order} is not a supported value.")
            };
        }
    }
}
