using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Application.ViewModels
{
    public sealed class PageResult<T>
    {
        public int Count { get; set; }

        public IList<T> Items { get; set; } = Array.Empty<T>();

        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public PageResult(int count, IList<T> items, int pageIndex, int pageSize)
        {
            Count = count;
            Items = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public PageResult()
        {

        }

        public static PageResult<T> Empty()
        {
            return new PageResult<T>
            {
                Count = 0,
                Items = Array.Empty<T>(),
                PageIndex = 1,
                PageSize = 10
            };
        }
    }
}
