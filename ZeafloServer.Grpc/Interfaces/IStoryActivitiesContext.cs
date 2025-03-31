using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.StoryActivities;

namespace ZeafloServer.Grpc.Interfaces
{
    public interface IStoryActivitiesContext
    {
        Task<IEnumerable<StoryActivityViewModel>> GetByIds(IEnumerable<Guid> ids);
    }
}
