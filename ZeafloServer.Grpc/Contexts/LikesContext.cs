using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.Likes;
using ZeafloServer.Shared.Likes;

namespace ZeafloServer.Grpc.Contexts
{
    public class LikesContext : ILikesContext
    {
        private readonly LikesApi.LikesApiClient _client;

        public LikesContext(LikesApi.LikesApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<LikeViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetLikesByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.Likes.Select(like => new LikeViewModel(
                Guid.Parse(like.Id),
                Guid.Parse(like.UserId),
                Guid.Parse(like.PostId)
            ));
        }
    }
}
