using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.PhotoPosts;
using ZeafloServer.Shared.PhotoPosts;
using AnnotationType = ZeafloServer.Shared.Enums.AnnotationType;

namespace ZeafloServer.Grpc.Contexts
{
    public class PhotoPostsContext : IPhotoPostsContext
    {
        private readonly PhotoPostsApi.PhotoPostsApiClient _client;

        public PhotoPostsContext(PhotoPostsApi.PhotoPostsApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<PhotoPostViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetPhotoPostsByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.PhotoPosts.Select(photoPost => new PhotoPostViewModel(
                Guid.Parse(photoPost.Id),
                Guid.Parse(photoPost.UserId),
                photoPost.ImageUrl,
                (AnnotationType)photoPost.AnnotationType,
                photoPost.AnnotationValue,
                photoPost.SentAt.ToDateTime()
            ));
        }
    }
}
