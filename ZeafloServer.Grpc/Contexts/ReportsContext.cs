using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.Reports;
using ZeafloServer.Shared.Enums;
using ZeafloServer.Shared.Reports;
using ReportStatus = ZeafloServer.Shared.Enums.ReportStatus;

namespace ZeafloServer.Grpc.Contexts
{
    public class ReportsContext : IReportsContext
    {
        private readonly ReportsApi.ReportsApiClient _client;

        public ReportsContext(ReportsApi.ReportsApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<ReportViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetReportsByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.Reports.Select(report => new ReportViewModel(
                Guid.Parse(report.Id),
                Guid.Parse(report.UserId),
                Guid.Parse(report.PostId),
                report.Reason,
                (ReportStatus)report.Status
            ));
        }
    }
}
