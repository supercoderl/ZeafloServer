using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Messages.UpdateUnreadMessage
{
    public sealed class UpdateUnreadMessageCommand : CommandBase<bool>, IRequest<bool>
    {
        private static readonly UpdateUnreadMessageCommandValidation s_validation = new();

        public Guid SenderId { get; }
        public Guid ReceiverId { get; }

        public UpdateUnreadMessageCommand(
            Guid senderId,
            Guid receiverId
        ) : base (Guid.NewGuid())
        {
            SenderId = senderId;
            ReceiverId = receiverId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
