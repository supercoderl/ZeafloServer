using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Posts;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface IPostsContext
    {
        Task<IEnumerable<PostViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
