using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.UserLevels;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface IUserLevelsContextcs
    {
        Task<IEnumerable<UserLevelViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
