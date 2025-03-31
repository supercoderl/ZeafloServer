using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Entities
{
    public class Message : Entity<Guid>
    {
        [Column("message_id")]
        public Guid MessageId { get; private set; }

        [Column("sender_id")]
        public Guid SenderId { get; private set; }

        [Column("receiver_id")]
        public Guid ReceiverId { get; private set; }

        [Column("content")]
        public string Content { get; private set; }

        [Column("media_url")]
        public string? MediaUrl { get; private set; }

        [Column("is_read")]
        public bool IsRead { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }

        [ForeignKey("SenderId")]
        [InverseProperty("SenderMessages")]
        public virtual User? Sender { get; set; }

        [ForeignKey("ReceiverId")]
        [InverseProperty("ReceiverMessages")]
        public virtual User? Receiver { get; set; }

        public Message(
            Guid messageId,
            Guid senderId,
            Guid receiverId,
            string content,
            string? mediaUrl,
            bool isRead,
            DateTime createdAt
        ) : base (messageId)
        {
            MessageId = messageId;
            SenderId = senderId;
            ReceiverId = receiverId;
            Content = content;
            MediaUrl = mediaUrl;
            IsRead = isRead;
            CreatedAt = createdAt;
        }

        public void SetSenderId( Guid senderId ) { SenderId = senderId; }
        public void SetReceiverId( Guid receiverId ) { ReceiverId = receiverId; }
        public void SetContent( string content ) { Content = content; }
        public void SetMediaUrl( string? mediaUrl ) { MediaUrl = mediaUrl; }
        public void SetIsRead( bool isRead ) { IsRead = isRead; }
        public void SetCreatedAt( DateTime createdAt ) { CreatedAt = createdAt; }
    }
}
