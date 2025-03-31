using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.MemberShipLevels;
using ZeafloServer.Shared.Enums;
using ZeafloServer.Shared.MemberShipLevels;
using LevelType = ZeafloServer.Shared.Enums.LevelType;

namespace ZeafloServer.Grpc.Contexts
{
    public class MemberShipLevelsContext : IMemberShipLevelsContext
    {
        private readonly MemberShipLevelsApi.MemberShipLevelsApiClient _client;

        public MemberShipLevelsContext(MemberShipLevelsApi.MemberShipLevelsApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<MemberShipLevelViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetMemberShipLevelsByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.MemberShipLevels.Select(memberShipLevel => new MemberShipLevelViewModel(
                Guid.Parse(memberShipLevel.Id),
                (LevelType)memberShipLevel.Type,
                memberShipLevel.MinPoint
            ));
        }
    }
}
