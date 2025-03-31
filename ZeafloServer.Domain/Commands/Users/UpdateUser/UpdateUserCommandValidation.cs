using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Users.UpdateUser
{
    public sealed class UpdateUserCommandValidation : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidation()
        {
            RuleForId();
            RuleForUsername();
            RuleForEmail();
        }

        private void RuleForId()
        {
            RuleFor(cmd => cmd.UserId).NotEmpty().WithErrorCode(DomainErrorCodes.User.EmptyId).WithMessage("Id may not be empty.");
        }

        private void RuleForUsername()
        {
            RuleFor(cmd => cmd.Username).NotEmpty().WithErrorCode(DomainErrorCodes.User.EmptyUsername).WithMessage("Username may not be empty.");
        }

        private void RuleForEmail()
        {
            RuleFor(cmd => cmd.Email).NotEmpty().WithErrorCode(DomainErrorCodes.User.EmptyEmail).WithMessage("Email may not be empty.");
        }
    }
}
