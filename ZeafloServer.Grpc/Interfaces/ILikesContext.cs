using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Likes;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface ILikesContext
    {
        Task<IEnumerable<LikeViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
