using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Messages.CreateMessage
{
    public sealed class CreateMessageCommandValidation : AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageCommandValidation()
        {
            RuleForContent();
        }

        private void RuleForContent()
        {
            RuleFor(cmd => cmd.Content).NotEmpty().WithErrorCode(DomainErrorCodes.Message.EmptyContent).WithMessage("Content may not be empty.");
        }
    }
}
