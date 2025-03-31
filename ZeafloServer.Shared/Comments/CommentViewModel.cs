using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Comments
{
    public sealed record CommentViewModel(
        Guid CommentId,
        Guid PostId,
        Guid UserId,
        string Content
    );
}
