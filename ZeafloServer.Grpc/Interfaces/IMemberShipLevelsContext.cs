using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.MemberShipLevels;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface IMemberShipLevelsContext
    {
        Task<IEnumerable<MemberShipLevelViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
