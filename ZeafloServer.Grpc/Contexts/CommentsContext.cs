using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.Comments;
using ZeafloServer.Shared.Comments;

namespace ZeafloServer.Grpc.Contexts
{
    public class CommentsContext : ICommentsContext
    {
        private readonly CommentsApi.CommentsApiClient _client;

        public CommentsContext(CommentsApi.CommentsApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<CommentViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetCommentsByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.Comments.Select(comment => new CommentViewModel(
                Guid.Parse(comment.Id),
                Guid.Parse(comment.PostId),
                Guid.Parse(comment.UserId),
                comment.Content
            ));
        }
    }
}
