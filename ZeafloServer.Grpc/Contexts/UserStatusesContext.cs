using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.Users;
using ZeafloServer.Proto.UserStatuses;
using ZeafloServer.Shared.Users;
using ZeafloServer.Shared.UserStatuses;

namespace ZeafloServer.Grpc.Contexts
{
    public class UserStatusesContext : IUserStatusesContext
    {
        private readonly UserStatusesApi.UserStatusesApiClient _client;

        public UserStatusesContext(UserStatusesApi.UserStatusesApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<UserStatusViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetUserStatusesByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.UserStatuses.Select(userStatus => new UserStatusViewModel(
                Guid.Parse(userStatus.Id),
                Guid.Parse(userStatus.UserId),
                userStatus.IsOnline,
                userStatus.LastSeen.ToDateTime()
            ));
        }
    }
}
