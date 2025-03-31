using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Application.ViewModels
{
    public sealed class PageQuery
    {
        private int _pageIndex = 1;
        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Max(0, value);
        }

        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = Math.Max(0, value);
        }
    }
}
