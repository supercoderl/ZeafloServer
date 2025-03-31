using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Cities;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface ICitiesContext
    {
        Task<IEnumerable<CityViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
