using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Tokens.UpdateToken
{
    public sealed class UpdateTokenCommand : CommandBase<Guid>, IRequest<Guid>
    {
        private static readonly UpdateTokenCommandValidation s_validation = new();

        public Guid TokenId { get; }
        public string AccessToken { get; }
        public string RefreshToken { get; }
        public Guid UserId { get; }
        public bool IsRefreshTokenRevoked { get; }
        public DateTime RefreshTokenExpiredDate { get; }

        public UpdateTokenCommand(
            Guid tokenId,
            string accessToken,
            string refreshToken,
            Guid userId,
            bool isRefreshTokenRevoked,
            DateTime refreshTokenExpiredDate
        ) : base(Guid.NewGuid())
        {
            TokenId = tokenId;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            UserId = userId;
            IsRefreshTokenRevoked = isRefreshTokenRevoked;
            RefreshTokenExpiredDate = refreshTokenExpiredDate;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
