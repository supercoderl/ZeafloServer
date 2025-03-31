using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.UserLevels;
using ZeafloServer.Proto.Users;
using ZeafloServer.Shared.UserLevels;
using ZeafloServer.Shared.Users;

namespace ZeafloServer.Grpc.Contexts
{
    public class UserLevelsContext : IUserLevelsContextcs
    {
        private readonly UserLevelsApi.UserLevelsApiClient _client;

        public UserLevelsContext(UserLevelsApi.UserLevelsApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<UserLevelViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetUserLevelsByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.UserLevels.Select(userLevel => new UserLevelViewModel(
                Guid.Parse(userLevel.Id),
                Guid.Parse(userLevel.UserId),
                Guid.Parse(userLevel.MemberShipLevelId),
                userLevel.ZeafloPoint,
                userLevel.AssignedAt.ToDateTime()
            ));
        }
    }
}
