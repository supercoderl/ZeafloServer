using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.SavePosts;
using ZeafloServer.Shared.SavePosts;

namespace ZeafloServer.Grpc.Contexts
{
    public class SavePostsContext : ISavePostsContext
    {
        private readonly SavePostsApi.SavePostsApiClient _client;

        public SavePostsContext(SavePostsApi.SavePostsApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<SavePostViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetSavePostsByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.SavePosts.Select(savePost => new SavePostViewModel(
                Guid.Parse(savePost.Id),
               Guid.Parse(savePost.UserId),
               Guid.Parse(savePost.PostId)
            ));
        }
    }
}
