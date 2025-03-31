using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Users.ForgotPassword
{
    public sealed class ForgotPasswordCommandValidation : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidation()
        {
            RuleForPhoneNumber();
        }

        private void RuleForPhoneNumber()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty().WithErrorCode(DomainErrorCodes.User.EmptyPhoneNumber).WithMessage("Phone number may not be empty.");
        }
    }
}
