using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Users;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface IUsersContext
    {
        Task<IEnumerable<UserViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
