using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.PlaceImages;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface IPlaceImagesContext
    {
        Task<IEnumerable<PlaceImageViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
