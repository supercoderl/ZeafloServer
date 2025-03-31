using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Enums;

namespace ZeafloServer.Shared.Posts
{
    public sealed record PostViewModel(
         Guid PostId,
         Guid UserId,
         string Title,
         string Content,
         string? ThumbnailUrl,
         Visibility Visibility
    );
}
