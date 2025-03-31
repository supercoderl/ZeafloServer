using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.MapThemes;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface IMapThemesContext
    {
        Task<IEnumerable<MapThemeViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
