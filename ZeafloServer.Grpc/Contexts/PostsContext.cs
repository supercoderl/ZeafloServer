using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.Posts;
using ZeafloServer.Proto.Users;
using ZeafloServer.Shared.Enums;
using ZeafloServer.Shared.Posts;
using ZeafloServer.Shared.Users;
using MediaType = ZeafloServer.Shared.Enums.MediaType;
using Visibility = ZeafloServer.Shared.Enums.Visibility;

namespace ZeafloServer.Grpc.Contexts
{
    public class PostsContext : IPostsContext
    {
        private readonly PostsApi.PostsApiClient _client;

        public PostsContext(PostsApi.PostsApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<PostViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetPostsByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.Posts.Select(post => new PostViewModel(
                Guid.Parse(post.Id),
                Guid.Parse(post.UserId),
                post.Title,
                post.Content,
                post.ThumbnailUrl,
                (Visibility)post.Visibility
            ));
        }
    }
}
