using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Tokens
{
    public sealed record TokenViewModel(
         Guid TokenId,
         string AccessToken,
         string RefreshToken,
         Guid UserId,
         bool IsRefreshTokenRevoked,
         DateTime RefreshTokenExpiredDate
    );
}
