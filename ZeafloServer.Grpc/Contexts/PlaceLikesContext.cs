using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.PlaceLikes;
using ZeafloServer.Shared.PlaceLikes;

namespace ZeafloServer.Grpc.Contexts
{
    public class PlaceLikesContext : IPlaceLikesContext
    {
        private readonly PlaceLikesApi.PlaceLikesApiClient _client;

        public PlaceLikesContext(PlaceLikesApi.PlaceLikesApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<PlaceLikeViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetPlaceLikesByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.PlaceLikes.Select(placeLike => new PlaceLikeViewModel(
                Guid.Parse(placeLike.Id),
                Guid.Parse(placeLike.PlaceId),
                Guid.Parse(placeLike.UserId)
            ));
        }
    }
}
