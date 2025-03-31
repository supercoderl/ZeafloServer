using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Entities
{
    public class PostReaction : Entity<Guid>
    {
        [Column("post_reaction_id")]
        public Guid PostReactionId { get; private set; }

        [Column("post_id")]
        public Guid PostId { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("reaction_type")]
        public ReactionType ReactionType { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }

        [ForeignKey("PostId")]
        [InverseProperty("PostReactions")]
        public virtual Post? Post { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("PostReactions")]
        public virtual User? User { get; set; }

        public PostReaction(
            Guid postReactionId,
            Guid postId,
            Guid userId,
            ReactionType reactionType,
            DateTime createdAt
        ) : base(postReactionId)
        {
            PostReactionId = postReactionId;
            PostId = postId;
            UserId = userId;
            ReactionType = reactionType;
            CreatedAt = createdAt;
        }

        public void SetPostId( Guid postId ) { PostId = postId; }
        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetReactionType( ReactionType reactionType ) { ReactionType = reactionType; }
        public void SetCreatedAt( DateTime createdAt ) { CreatedAt = createdAt; }
    }
}
