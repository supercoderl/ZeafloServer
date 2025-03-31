using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.PasswordResetTokens;
using ZeafloServer.Shared.PasswordResetTokens;

namespace ZeafloServer.Grpc.Contexts
{
    public class PasswordResetTokensContext : IPasswordResetTokensContext
    {
        private readonly PasswordResetTokensApi.PasswordResetTokensApiClient _client;

        public PasswordResetTokensContext(PasswordResetTokensApi.PasswordResetTokensApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<PasswordResetTokenViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetPasswordResetTokensByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.PasswordResetTokens.Select(passwordResetToken => new PasswordResetTokenViewModel(
                Guid.Parse(passwordResetToken.Id),
                Guid.Parse(passwordResetToken.UserId),
                passwordResetToken.Code,
                passwordResetToken.ExpiresAt.ToDateTime(),
                passwordResetToken.AttempCount,
                passwordResetToken.IsUsed
            ));
        }
    }
}
