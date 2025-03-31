using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Users.Login
{
    public sealed class LoginCommandValidation : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidation()
        {
            RuleForIdentifier();
            RuleForPassword();
        }

        public void RuleForIdentifier()
        {
            RuleFor(cmd => cmd.Identifier).NotEmpty().WithMessage("Identifier may not be empty.");
        }

        public void RuleForPassword()
        {
            RuleFor(cmd => cmd.Password).NotEmpty().WithErrorCode(DomainErrorCodes.User.EmptyPassword).WithMessage("Password may not be empty.");
        }
    }
}
