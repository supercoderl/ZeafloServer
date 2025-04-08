using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.PhotoPosts;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface IPhotoPostsContext
    {
        Task<IEnumerable<PhotoPostViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
