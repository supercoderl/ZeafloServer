using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Processes.UpdateProcess
{
    public sealed class UpdateProcessCommandValidation : AbstractValidator<UpdateProcessCommand>
    {
        public UpdateProcessCommandValidation()
        {
            RuleForId();
            RuleForUserId();
            RuleForType();
        }

        private void RuleForId()
        {
            RuleFor(cmd => cmd.ProcessingId).NotEmpty().WithErrorCode(DomainErrorCodes.Process.EmptyProcessingId).WithMessage("Id may not be empty.");
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
