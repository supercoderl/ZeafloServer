using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Tokens.UpdateToken
{
    public sealed class UpdateTokenCommandValidation : AbstractValidator<UpdateTokenCommand>
    {
        public UpdateTokenCommandValidation()
        {
            RuleForId();
            RuleForAccessToken();
            RuleForRefreshToken();
        }

        private void RuleForId()
        {
            RuleFor(cmd => cmd.TokenId).NotEmpty().WithErrorCode(DomainErrorCodes.Token.EmptyId).WithMessage("Id may not be empty.");
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
