using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Entities
{
    public class Token : Entity<Guid>
    {
        [Column("token_id")]
        public Guid TokenId { get; private set; }

        [Column("access_token")]
        public string AccessToken { get; private set; }

        [Column("refresh_token")]
        public string RefreshToken { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("is_refresh_token_revoked")]
        public bool IsRefreshTokenRevoked { get; private set; }

        [Column("refresh_token_expired_date")]
        public DateTime RefreshTokenExpiredDate { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("Tokens")]
        public virtual User? User { get; set; }

        public Token(
            Guid tokenId,
            string accessToken,
            string refreshToken,
            Guid userId,
            bool isRefreshTokenRevoked,
            DateTime refreshTokenExpiredDate
        ) : base(tokenId)
        {
            TokenId = tokenId;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            UserId = userId;
            IsRefreshTokenRevoked = isRefreshTokenRevoked;
            RefreshTokenExpiredDate = refreshTokenExpiredDate;
        }

        public void SetAccessToken( string accessToken ) { AccessToken = accessToken; }
        public void SetRefreshToken( string refreshToken ) { RefreshToken = refreshToken; }
        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetIsRefreshTokenRevoked(bool isRefreshTokenRevoked) { IsRefreshTokenRevoked = isRefreshTokenRevoked; }
        public void SetRefreshTokenExpiredDate(DateTime refreshTokenExpiredDate) { RefreshTokenExpiredDate = refreshTokenExpiredDate; }
    }
}
