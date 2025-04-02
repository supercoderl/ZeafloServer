using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Enums
{
    public enum FriendShipStatus
    {
        Pending,
        Accepted,
        Blocked,
        None
    }

    public enum ReportStatus
    {
        Pending,
        Resolved,
        Rejected,
    }

    public enum ActionStatus
    {
        All,
        Deleted,
        NotDeleted,
    }

    public enum ProcessStatus
    {
        Pending, 
        InProgress, 
        Completed, 
        Failed
    }
}
