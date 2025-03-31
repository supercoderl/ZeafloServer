using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Entities
{
    public class PostMedia : Entity<Guid>
    {
        [Column("post_media_id")]
        public Guid PostMediaId { get; private set; }

        [Column("post_id")]
        public Guid PostId { get; private set; }

        [Column("media_url")]
        public string MediaUrl { get; private set; }

        [Column("media_type")]
        public MediaType MediaType { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }

        [ForeignKey("PostId")]
        [InverseProperty("PostMedias")]
        public virtual Post? Post { get; set; }

        public PostMedia(
            Guid postMediaId,
            Guid postId,
            string mediaUrl,
            MediaType mediaType,
            DateTime createdAt
        ) : base(postMediaId )  
        {
            PostMediaId = postMediaId;
            PostId = postId;
            MediaUrl = mediaUrl;
            MediaType = mediaType;
            CreatedAt = createdAt;
        }

        public void SetPostId(Guid postId) { PostId = postId; }
        public void SetMediaUrl(string mediaUrl) { MediaUrl = mediaUrl; }
        public void SetMediaType(MediaType mediaType) { MediaType = mediaType; }
        public void SetCreatedAt(DateTime createdAt) { CreatedAt = createdAt; }

    }
}
