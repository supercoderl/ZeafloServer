using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Tokens;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface ITokensContext
    {
        Task<IEnumerable<TokenViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
