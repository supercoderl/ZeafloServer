using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Users.ChangePassword
{
    public sealed class ChangePasswordCommandValidation : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidation()
        {
            RuleForId();
            RuleForOldPassword();
            RuleForNewPassword();
        }

        private void RuleForId()
        {
            RuleFor(cmd => cmd.UserId).NotEmpty().WithErrorCode(DomainErrorCodes.User.EmptyId).WithMessage("Id may not be empty.");
        }

        private void RuleForOldPassword()
        {
            RuleFor(cmd => cmd.OldPassword).NotEmpty().WithErrorCode(DomainErrorCodes.User.EmptyOldPassword).WithMessage("Old password may not be empty.");
        }

        private void RuleForNewPassword()
        {
            RuleFor(cmd => cmd.NewPassword).NotEmpty().WithErrorCode(DomainErrorCodes.User.EmptyNewPassword).WithMessage("New password may not be empty.");
        }

    }
}
