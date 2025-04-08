using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Enums;

namespace ZeafloServer.Shared.PhotoPosts
{
    public sealed record PhotoPostViewModel
    (
        Guid PhotoPostId,
        Guid UserId,
        string ImageUrl,
        AnnotationType? AnnotationType,
        string? AnnotationValue,
        DateTime SentAt
    );
}
