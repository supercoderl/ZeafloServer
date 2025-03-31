using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Users.ResetPassword
{
    public sealed class ResetPasswordCommandValidation : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidation()
        {
            RuleForNewPassword();
            RuleForToken();
        }

        private void RuleForNewPassword()
        {
            RuleFor(x => x.NewPassword).NotEmpty().WithErrorCode(DomainErrorCodes.User.EmptyPassword).WithMessage("Password may not be empty.");
        }

        private void RuleForToken()
        {
            RuleFor(x => x.Token).NotEmpty().WithMessage("Token may not be empty.");
        }
    }
}
