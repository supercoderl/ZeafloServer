using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.Queries.FriendShips.GetAll;
using ZeafloServer.Application.Queries.FriendShips.GetContacts;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.FriendShips;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Commands.FriendShips.AcceptRequest;
using ZeafloServer.Domain.Commands.FriendShips.AddFriend;
using ZeafloServer.Domain.Commands.FriendShips.CancelRequest;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Application.Services
{
    public class FriendShipService : IFriendShipService
    {
        private readonly IMediatorHandler _bus;

        public FriendShipService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<bool> AcceptRequest(AcceptRequest request)
        {
            return await _bus.SendCommandAsync(new AcceptRequestCommand(request.FriendShipId));
        }

        public async Task<Guid> AddFriend(AddFriendRequest request)
        {
            return await _bus.SendCommandAsync(new AddFriendCommand(
                Guid.NewGuid(),
                request.UserId,
                request.FriendId
            ));
        }

        public async Task<bool> CancelRequest(CancelRequest request)
        {
            return await _bus.SendCommandAsync(new CancelRequestCommand(request.FriendShipId, request.UserId, request.FriendId));
        }

        public async Task<PageResult<FriendShipViewModel>> GetAllFriendShipsAsync(PageQuery query, ActionStatus status, string actionType = "", string searchTerm = "", Guid? userId = null)
        {
            return await _bus.QueryAsync(new GetAllFriendShipsQuery(query, status, userId, actionType, searchTerm));
        }

        public async Task<PageResult<ContactInfo>> GetContactsAsync(PageQuery query, ActionStatus status, Guid userId, string searchTerm = "")
        {
            return await _bus.QueryAsync(new GetContactsQuery(query, status, userId));
        }
    }
}
