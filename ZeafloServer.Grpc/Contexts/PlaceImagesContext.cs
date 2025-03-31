using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.PlaceImages;
using ZeafloServer.Shared.PlaceImages;

namespace ZeafloServer.Grpc.Contexts
{
    public class PlaceImagesContext : IPlaceImagesContext
    {
        private readonly PlaceImagesApi.PlaceImagesApiClient _client;

        public PlaceImagesContext(PlaceImagesApi.PlaceImagesApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<PlaceImageViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetPlaceImagesByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.PlaceImages.Select(placeImage => new PlaceImageViewModel(
                Guid.Parse(placeImage.Id),
                Guid.Parse(placeImage.PlaceId),
                placeImage.ImageUrl
            ));
        }
    }
}
