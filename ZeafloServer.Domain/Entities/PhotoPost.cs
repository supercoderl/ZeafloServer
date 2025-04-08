using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Entities
{
    public class PhotoPost : Entity<Guid>
    {
        [Column("photo_post_id")]
        public Guid PhotoPostId { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("image_url")]
        public string ImageUrl { get; private set; }

        [Column("annotation_type")]
        public AnnotationType? AnnotationType { get; private set; }

        [Column("annotation_value")]
        public string? AnnotationValue { get; private set; }

        [Column("sent_at")]
        public DateTime SentAt { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("PhotoPosts")]
        public virtual User? User { get; set; }

        public PhotoPost(
            Guid photoPostId,
            Guid userId,
            string imageUrl,
            AnnotationType? annotationType,
            string? annotationValue,
            DateTime sentAt
        ) : base(photoPostId)
        {
            PhotoPostId = photoPostId;
            UserId = userId;
            ImageUrl = imageUrl;
            AnnotationType = annotationType;
            AnnotationValue = annotationValue;
            SentAt = sentAt;
        }

        public void SetUserId(Guid userId) { UserId = userId; }
        public void SetImageUrl(string imageUrl) { ImageUrl = imageUrl; }
        public void SetAnnotationType(AnnotationType? annotationType) { AnnotationType = annotationType; }
        public void SetAnnotationValue(string? annotationValue) { AnnotationValue = annotationValue; }
        public void SetSentAt(DateTime sentAt) { SentAt = sentAt; }
    }
}
