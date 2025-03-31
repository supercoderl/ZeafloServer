using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.ViewModels.Posts
{
    public sealed class PostViewModel
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ThumbnailUrl { get; set; }
        public Visibility Visibility { get; set; }
        public DateTime CreatedAt { get; set; }
        public Author Author { get; set; } = new Author(string.Empty, string.Empty, string.Empty);

        public static PostViewModel FromPost(Post post, Author author)
        {
            return new PostViewModel
            {
                PostId = post.Id,
                UserId = post.UserId,
                Title = post.Title,
                Content = post.Content,
                ThumbnailUrl = post.ThumbnailUrl,
                Visibility = post.Visibility,
                CreatedAt = post.CreatedAt,
                Author = author
            };
        }
    }

    public class Author
    {
        public string Fullname { get; set; }
        public string Location { get; set; }
        public string AvatarUrl { get; set; }

        public Author(
            string fullname,
            string location,
            string avatarUrl
        )
        {
            Fullname = fullname;
            Location = location;
            AvatarUrl = avatarUrl;
        }
    }
}
