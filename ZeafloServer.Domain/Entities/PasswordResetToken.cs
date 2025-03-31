using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Entities
{
    public class PasswordResetToken : Entity<Guid>
    {
        [Column("password_reset_token_id")]
        public Guid PasswordResetTokenId { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("code")]
        public string Code { get; private set; }

        [Column("expires_at")]
        public DateTime ExpiresAt { get; private set; }

        [Column("attempt_count")]
        public int AttemptCount { get; private set; }

        [Column("is_used")]
        public bool IsUsed { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("PasswordResetTokens")]
        public virtual User? User { get; set; }

        public PasswordResetToken(
            Guid passwordResetTokenId,
            Guid userId,
            string code,
            DateTime expiresAt,
            int attemptCount,
            bool isUsed
        ) : base(passwordResetTokenId)
        {
            PasswordResetTokenId = passwordResetTokenId;
            UserId = userId;
            Code = code;
            ExpiresAt = expiresAt;
            AttemptCount = attemptCount;
            IsUsed = isUsed;
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetCode( string code ) { Code = code; }
        public void SetExpiresAt( DateTime expiresAt ) { ExpiresAt = expiresAt; }
        public void SetAttemptCount( int attemptCount ) { AttemptCount = attemptCount; }
        public void SetIsUsed( bool isUsed ) { IsUsed = isUsed; }
    }
}
