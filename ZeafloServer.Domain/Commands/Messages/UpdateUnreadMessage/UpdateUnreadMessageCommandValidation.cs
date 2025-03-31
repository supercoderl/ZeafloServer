using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Messages.UpdateUnreadMessage
{
    public sealed class UpdateUnreadMessageCommandValidation : AbstractValidator<UpdateUnreadMessageCommand>
    {
        public UpdateUnreadMessageCommandValidation()
        {
            RuleForSenderId();
            RuleForReceiverId();
        }

        private void RuleForSenderId()
        {
            RuleFor(cmd => cmd.SenderId).NotEmpty().WithErrorCode(DomainErrorCodes.Message.EmptySenderId).WithMessage("Sender id may not be empty.");
        }

        private void RuleForReceiverId()
        {
            RuleFor(cmd => cmd.ReceiverId).NotEmpty().WithErrorCode(DomainErrorCodes.Message.EmptyReceiverId).WithMessage("Receiver id may not be empty.");
        }
    }
}
