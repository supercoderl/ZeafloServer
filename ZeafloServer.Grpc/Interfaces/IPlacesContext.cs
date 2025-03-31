using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Places;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface IPlacesContext
    {
        Task<IEnumerable<PlaceViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
