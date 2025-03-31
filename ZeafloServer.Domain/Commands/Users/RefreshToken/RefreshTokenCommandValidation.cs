using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Users.RefreshToken
{
    public sealed class RefreshTokenCommandValidation : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidation()
        {
            RuleForRefreshToken();
        }

        private void RuleForRefreshToken()
        {
            RuleFor(cmd => cmd.RefreshToken).NotEmpty().WithErrorCode(DomainErrorCodes.Token.EmptyRefreshToken).WithMessage("Refresh token may not be empty.");
        }
    }
}
