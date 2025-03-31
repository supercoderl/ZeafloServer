using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.Cities;
using ZeafloServer.Shared.Cities;

namespace ZeafloServer.Grpc.Contexts
{
    public class CitiesContext : ICitiesContext
    {
        private readonly CitiesApi.CitiesApiClient _client;

        public CitiesContext(CitiesApi.CitiesApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<CityViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetCitiesByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.Cities.Select(city => new CityViewModel(
                Guid.Parse(city.Id),
                city.Name,
                city.PostalCode,
                city.Latitude,
                city.Longitude
            ));
        }
    }
}
