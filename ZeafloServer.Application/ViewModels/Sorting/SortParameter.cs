using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Application.ViewModels.Sorting
{
    public readonly struct SortParameter
    {
        public SortOrder Order { get; }
        public string ParameterName { get; }

        public SortParameter(string parameterName, SortOrder order)
        {
            ParameterName = parameterName;
            Order = order;
        }
    }
}
