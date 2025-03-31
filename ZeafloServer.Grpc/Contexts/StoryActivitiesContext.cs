using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.StoryActivities;
using ZeafloServer.Shared.Enums;
using ZeafloServer.Shared.StoryActivities;
using ActionType = ZeafloServer.Shared.Enums.ActionType;

namespace ZeafloServer.Grpc.Contexts
{
    public class StoryActivitiesContext : IStoryActivitiesContext
    {
        private readonly StoryActivitiesApi.StoryActivitiesApiClient _client;

        public StoryActivitiesContext(StoryActivitiesApi.StoryActivitiesApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<StoryActivityViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetStoryActivitiesByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.StoryActivities.Select(storyActivity => new StoryActivityViewModel(
                Guid.Parse(storyActivity.Id),
                Guid.Parse(storyActivity.UserId),
                (ActionType)storyActivity.ActionType,
                storyActivity.PointEarned
            ));
        }
    }
}
