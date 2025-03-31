using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.Tokens;
using ZeafloServer.Proto.Users;
using ZeafloServer.Shared.Tokens;
using ZeafloServer.Shared.Users;

namespace ZeafloServer.Grpc.Contexts
{
    public class TokensContext : ITokensContext
    {
        private readonly TokensApi.TokensApiClient _client;

        public TokensContext(TokensApi.TokensApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<TokenViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetTokensByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.Tokens.Select(token => new TokenViewModel(
                Guid.Parse(token.Id),
                token.AccessToken,
                token.RefreshToken,
                Guid.Parse(token.UserId),
                token.IsRefreshTokenRevoked,
                token.RefreshTokenExpiredDate.ToDateTime()
            ));
        }
    }
}
