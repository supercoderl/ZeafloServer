using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.MemberShipLevels;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.SortProviders
{
    public sealed class MemberShipLevelViewModelSortProvider : ISortingExpressionProvider<MemberShipLevelViewModel, MemberShipLevel>
    {
        private static readonly Dictionary<string, Expression<Func<MemberShipLevel, object>>> s_expressions = new()
        {
            { "minPoint", memberShipLevel => memberShipLevel.MinPoint }
        };

        public Dictionary<string, Expression<Func<MemberShipLevel, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
