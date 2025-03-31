using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.PasswordResetTokens
{
    public sealed record PasswordResetTokenViewModel(
         Guid PasswordResetTokenId,
         Guid UserId,
         string Code,
         DateTime ExpiresAt,
         int AttemptCount,
         bool IsUsed
    );
}
