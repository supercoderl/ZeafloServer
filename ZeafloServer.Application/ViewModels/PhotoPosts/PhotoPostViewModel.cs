using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.ViewModels.PhotoPosts
{
    public sealed class PhotoPostViewModel
    {
        public Guid PhotoPostId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string? AnnotationType { get; set; }
        public string? AnnotationValue { get; set; }
        public DateTime SentAt { get; set; }
        public UserInfo UserInfo { get; set; } = new UserInfo();

        public static PhotoPostViewModel FromPhotoPost(PhotoPost photoPost)
        {
            return new PhotoPostViewModel
            {
                PhotoPostId = photoPost.PhotoPostId,
                ImageUrl = photoPost.ImageUrl,
                AnnotationType = photoPost.AnnotationType?.ToString(),
                AnnotationValue = photoPost.AnnotationValue,
                SentAt = photoPost.SentAt,
                UserInfo = new UserInfo
                {
                    UserId = photoPost.UserId,
                    Fullname = photoPost.User?.Fullname ?? string.Empty,
                    Avatar = photoPost.User?.AvatarUrl ?? string.Empty
                }
            };
        }
    }

    public class UserInfo
    {
        public Guid UserId { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
    }
}
