using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.MapThemes;
using ZeafloServer.Proto.Users;
using ZeafloServer.Shared.MapThemes;
using ZeafloServer.Shared.Users;

namespace ZeafloServer.Grpc.Contexts
{
    public class MapThemesContext : IMapThemesContext
    {
        private readonly MapThemesApi.MapThemesApiClient _client;

        public MapThemesContext(MapThemesApi.MapThemesApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<MapThemeViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetMapThemesByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.MapThemes.Select(mapTheme => new MapThemeViewModel(
                Guid.Parse(mapTheme.Id),
                mapTheme.Name,
                mapTheme.Description,
                mapTheme.MapStyle,
                mapTheme.PreviewUrl,
                mapTheme.IsPremium
            ));
        }
    }
}
