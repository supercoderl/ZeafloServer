using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.ViewModels.Posts
{
    public sealed record CreatePostRequest(
        Guid UserId,
        string Title,
        string Content,
        string? ThumbnailUrl,
        Visibility Visibility
    );
}
