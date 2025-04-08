using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.ViewModels.PhotoPosts
{
    public sealed record CreatePhotoPostRequest
    (
        Guid UserId,
        string Image,
        string? AnnotationType,
        string? AnnotationValue
    );
}
