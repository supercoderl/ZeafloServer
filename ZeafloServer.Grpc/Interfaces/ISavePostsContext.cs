using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.SavePosts;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface ISavePostsContext
    {
        Task<IEnumerable<SavePostViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
