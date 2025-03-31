using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.ViewModels.Messages
{
    public sealed class MessageViewModel
    {
        public Guid MessageId { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? MediaUrl { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public static MessageViewModel FromMessage(Message message)
        {
            return new MessageViewModel
            {
                MessageId = message.MessageId,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                Content = message.Content,
                MediaUrl = message.MediaUrl,
                IsRead = message.IsRead,
                CreatedAt = message.CreatedAt,
            };
        }
    }
}
