using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.Users;
using ZeafloServer.Shared.Enums;
using ZeafloServer.Shared.Users;
using Gender = ZeafloServer.Shared.Enums.Gender;

namespace ZeafloServer.Grpc.Contexts
{
    public class UsersContext : IUsersContext
    {
        private readonly UsersApi.UsersApiClient _client;

        public UsersContext(UsersApi.UsersApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<UserViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetUsersByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.Users.Select(user => new UserViewModel(
                Guid.Parse(user.Id),
                user.Username,
                user.Email,
                user.Fullname,
                user.Bio,
                user.AvatarUrl,
                user.CoverPhotoUrl,
                user.PhoneNumber,
                user.Website,
                user.Location,
                user.QrUrl,
                user.Birthdate.ToDateTime(),
                (Gender)user.Gender,
                user.IsOnline,
                user.LastLoginTime.ToDateTime()
            ));
        }
    }
}
