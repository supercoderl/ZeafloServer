using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.PasswordResetTokens;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface IPasswordResetTokensContext
    {
        Task<IEnumerable<PasswordResetTokenViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
