using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.Queries.Users.GetLevel;
using ZeafloServer.Application.ViewModels.UserLevels;
using ZeafloServer.Domain.Commands.Points.AddPoint;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Application.Services
{
    public class UserLevelService : IUserLevelService
    {
        private readonly IMediatorHandler _bus;

        public UserLevelService(
            IMediatorHandler bus
        )
        {
            _bus = bus;
        }

        public async Task<Guid> AddPointAsync(AddPointRequest request)
        {
            return await _bus.SendCommandAsync(new AddPointCommand(
                request.UserId,
                request.ActionType
            ));
        }

        public async Task<UserLevelViewModel?> GetUserLevelAsync(Guid userId)
        {
            return await _bus.QueryAsync(new GetUserLevelQuery(userId));
        }
    }
}
