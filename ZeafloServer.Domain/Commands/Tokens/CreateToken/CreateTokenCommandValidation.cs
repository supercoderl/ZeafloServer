using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Tokens.CreateToken
{
    public sealed class CreateTokenCommandValidation : AbstractValidator<CreateTokenCommand>
    {
        public CreateTokenCommandValidation()
        {
            RuleForAccessToken();
            RuleForRefreshToken();
        }

        private void RuleForAccessToken()
        {
            RuleFor(cmd => cmd.AccessToken).NotEmpty().WithErrorCode(DomainErrorCodes.Token.EmptyAccessToken).WithMessage("Access token may not be empty.");
        }

        private void RuleForRefreshToken()
        {
            RuleFor(cmd => cmd.RefreshToken).NotEmpty().WithErrorCode(DomainErrorCodes.Token.EmptyRefreshToken).WithMessage("Refresh token may not be empty.");
        }
    }
}
