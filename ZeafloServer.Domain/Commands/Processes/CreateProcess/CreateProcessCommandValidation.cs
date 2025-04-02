using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Processes.CreateProcess
{
    public sealed class CreateProcessCommandValidation : AbstractValidator<CreateProcessCommand>
    {
        public CreateProcessCommandValidation()
        {
            RuleForUserId();
            RuleForType();
        }

        private void RuleForUserId()
        {
            RuleFor(cmd => cmd.UserId).NotEmpty().WithErrorCode(DomainErrorCodes.Process.EmptyUserId).WithMessage("User id may not be empty.");
        }

        private void RuleForType()
        {
            RuleFor(cmd => cmd.Type).NotEmpty().WithErrorCode(DomainErrorCodes.Process.EmptyType).WithMessage("Type may not be empty.");
        }
    }
}
