using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Entities
{
    public class Post : Entity<Guid>
    {
        [Column("post_id")]
        public Guid PostId { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("title")]
        public string Title { get; private set; }

        [Column("content")]
        public string Content { get; private set; }

        [Column("thumbnail_url")]
        public string? ThumbnailUrl { get; private set; }

        [Column("visibility")]
        public Visibility Visibility { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("Posts")]
        public virtual User? User { get; set; }

        [InverseProperty("Post")]
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        [InverseProperty("Post")]
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

        [InverseProperty("Post")]
        public virtual ICollection<SavePost> SavePosts { get; set; } = new List<SavePost>();

        [InverseProperty("Post")]
        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

        [InverseProperty("Post")]
        public virtual ICollection<PostReaction> PostReactions { get; set; } = new List<PostReaction>();

        [InverseProperty("Post")]
        public virtual ICollection<PostMedia> PostMedias { get; set; } = new List<PostMedia>();

        public Post(
            Guid postId,
            Guid userId,
            string title,
            string content,
            string? thumbnailUrl,
            Visibility visibility,
            DateTime createdAt
        ) : base(postId)
        {
            PostId = postId;
            UserId = userId;
            Title = title;
            Content = content;
            ThumbnailUrl = thumbnailUrl;
            Visibility = visibility;
            CreatedAt = createdAt;
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetTitle( string title ) { Title = title; }
        public void SetContent( string content ) { Content = content; }
        public void SetThumbnailUrl(string? thumbnailUrl) { ThumbnailUrl = thumbnailUrl; }
        public void SetVisibility( Visibility visibility ) { Visibility = visibility; }
        public void SetCreatedAt( DateTime createdAt ) { CreatedAt = createdAt; }
    }
}
