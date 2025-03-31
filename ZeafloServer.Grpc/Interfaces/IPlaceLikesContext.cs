using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.PlaceLikes;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface IPlaceLikesContext
    {
        Task<IEnumerable<PlaceLikeViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
