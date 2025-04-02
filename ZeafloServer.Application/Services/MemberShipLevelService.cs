using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.Queries.MemberShipLevels.GetAll;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.MemberShipLevels;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Commands.MemberShipLevels.CreateMemberShipLevel;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Application.Services
{
    public class MemberShipLevelService : IMemberShipLevelService
    {
        private readonly IMediatorHandler _bus;

        public MemberShipLevelService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<Guid> CreateMemberShipLevelAsync(CreateMemberShipLevelRequest request)
        {
            return await _bus.SendCommandAsync(new CreateMemberShipLevelCommand(
                Guid.NewGuid(),
                request.Type,
                request.MinPoint
            ));
        }

        public async Task<PageResult<MemberShipLevelViewModel>> GetAllMemberShipLevelsAsync(PageQuery query, ActionStatus status, string searchTerm = "", SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllMemberShipLevelsQuery(query, status, searchTerm, sortQuery));
        }
    }
}
