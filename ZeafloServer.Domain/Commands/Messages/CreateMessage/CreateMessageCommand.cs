using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Domain.Commands.Messages.CreateMessage
{
    public sealed class CreateMessageCommand : CommandBase<Message?>, IRequest<Message?>
    {
        private static readonly CreateMessageCommandValidation s_validation = new();

        public Guid MessageId { get; }
        public Guid SenderId { get; }
        public Guid ReceiverId { get; }
        public string Content { get; }
        public string? MediaUrl { get; }

        public CreateMessageCommand(
            Guid messageId,
            Guid senderId,
            Guid receiverId,
            string content,
            string? mediaUrl
        ) : base(Guid.NewGuid())
        {
            MessageId = messageId;
            SenderId = senderId;
            ReceiverId = receiverId;
            Content = content;
            MediaUrl = mediaUrl;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
