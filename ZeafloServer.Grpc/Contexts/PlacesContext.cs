using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.Places;
using ZeafloServer.Shared.Enums;
using ZeafloServer.Shared.Places;
using PlaceType = ZeafloServer.Shared.Enums.PlaceType;

namespace ZeafloServer.Grpc.Contexts
{
    public class PlacesContext : IPlacesContext
    {
        private readonly PlacesApi.PlacesApiClient _client;

        public PlacesContext(PlacesApi.PlacesApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<PlaceViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetPlacesByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.Places.Select(place => new PlaceViewModel(
                Guid.Parse(place.Id),
                place.Name,
                place.Address,
                place.Description,
                (PlaceType)place.Type,
                Guid.Parse(place.CityId),
                place.Latitude,
                place.Longitude,
                place.Rating,
                place.ReviewCount,
                place.IsOpen
            ));
        }
    }
}
