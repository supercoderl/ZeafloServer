using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Enums;

namespace ZeafloServer.Shared.Reports
{
    public sealed record ReportViewModel(
         Guid ReportId,
         Guid UserId,
         Guid PostId,
         string Reason,
         ReportStatus Status
    );
}
