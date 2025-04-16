using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.UserLevels;

namespace ZeafloServer.Application.Interfaces
{
    public interface IUserLevelService
    {
        public Task<UserLevelViewModel?> GetUserLevelAsync(Guid userId);
        public Task<Guid> AddPointAsync(AddPointRequest request);
    }
}
