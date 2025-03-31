using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Reports;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface IReportsContext
    {
        Task<IEnumerable<ReportViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
