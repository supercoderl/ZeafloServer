using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Entities
{
    public class SavePost : Entity<Guid>
    {
        [Column("save_post_id")]
        public Guid SavePostId { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("post_id")]
        public Guid PostId { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("SavePosts")]
        public virtual User? User { get; set; }

        [ForeignKey("PostId")]
        [InverseProperty("SavePosts")]
        public virtual Post? Post { get; set; }

        public SavePost(
            Guid savePostId,
            Guid userId,
            Guid postId,
            DateTime createdAt
        ) : base(savePostId )
        {
            SavePostId = savePostId;
            UserId = userId;
            PostId = postId;
            CreatedAt = createdAt;
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetPostId( Guid postId ) { PostId = postId; }
        public void SetCreatedAt( DateTime createdAt ) { CreatedAt = createdAt; }
    }
}
