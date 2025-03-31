using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Comments;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface ICommentsContext
    {
        Task<IEnumerable<CommentViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
