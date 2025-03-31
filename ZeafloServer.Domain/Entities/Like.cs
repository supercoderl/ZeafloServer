using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Entities
{
    public class Like : Entity<Guid>
    {
        [Column("like_id")]
        public Guid LikeId { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("post_id")]
        public Guid PostId { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("Likes")]
        public virtual User? User { get; set; }

        [ForeignKey("PostId")]
        [InverseProperty("Likes")]
        public virtual Post? Post { get; set; }

        public Like(
            Guid likeId,
            Guid userId,
            Guid postId,
            DateTime createdAt
        ) : base( likeId )
        {
            LikeId = likeId;
            UserId = userId;
            PostId = postId;
            CreatedAt = createdAt;
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetPostId ( Guid postId ) { PostId = postId; }
        public void SetCreatedAt( DateTime createdAt ) { CreatedAt = createdAt; }
    }
}
