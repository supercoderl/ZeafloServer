using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.UserStatuses;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface IUserStatusesContext
    {
        Task<IEnumerable<UserStatusViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
