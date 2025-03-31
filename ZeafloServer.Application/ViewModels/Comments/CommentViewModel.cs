using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.ViewModels.Comments
{
    public sealed class CommentViewModel
    {
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public static CommentViewModel FromComment(Comment comment)
        {
            return new CommentViewModel
            {
                CommentId = comment.CommentId,
                PostId = comment.PostId,
                CreatedAt = comment.CreatedAt,
                Content = comment.Content,
                UserId = comment.UserId,
            };
        }
    }
}
