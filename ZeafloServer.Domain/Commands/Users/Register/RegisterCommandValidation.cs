using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Users.Register
{
    public sealed class RegisterCommandValidation : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidation()
        {
            RuleForUsername();
            RuleForEmail();
            RuleForPassword();
        }

        private void RuleForUsername()
        {
            RuleFor(cmd => cmd.Username).NotEmpty().WithErrorCode(DomainErrorCodes.User.EmptyUsername).WithMessage("Username may not be empty.");
        }

        private void RuleForEmail()
        {
            RuleFor(cmd => cmd.Email).NotEmpty().WithErrorCode(DomainErrorCodes.User.EmptyEmail).WithMessage("Email may not be empty.");
        }

        private void RuleForPassword()
        {
            RuleFor(cmd => cmd.Password).NotEmpty().WithErrorCode(DomainErrorCodes.User.EmptyPassword).WithMessage("Password may not be empty.");
        }
    }
}
