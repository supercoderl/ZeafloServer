using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Entities
{
    public class Comment : Entity<Guid>
    {
        [Column("comment_id")]
        public Guid CommentId { get; private set; }

        [Column("post_id")]
        public Guid PostId { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("content")]
        public string Content { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("Comments")]
        public virtual User? User { get; set; }

        [ForeignKey("PostId")]
        [InverseProperty("Comments")]
        public virtual Post? Post { get; set; }

        public Comment(
           Guid commentId,
           Guid postId,
           Guid userId,
           string content,
           DateTime createdAt
        ) : base(commentId)
        {
            CommentId = commentId;
            PostId = postId;
            UserId = userId;
            Content = content;
            CreatedAt = createdAt;
        }

        public void SetPostId( Guid postId ) { PostId = postId; }
        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetContent( string content ) { Content = content; }
        public void SetCreatedAt( DateTime createdAt ) { CreatedAt = createdAt; }
    }
}
