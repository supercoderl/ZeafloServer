using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Application.ViewModels.FriendShips;

namespace ZeafloServer.Application.Interfaces
{
    public interface IFriendShipService
    {
        public Task<PageResult<FriendShipViewModel>> GetAllFriendShipsAsync(
            PageQuery query,
            ActionStatus status,
            string actionType = "",
            string searchTerm = "",
            Guid? userId = null
        );
        public Task<PageResult<ContactInfo>> GetContactsAsync(
            PageQuery query,
            ActionStatus status,
            Guid userId,
            string searchTerm = ""
        );
        public Task<Guid> AddFriend(AddFriendRequest request);
        public Task<bool> CancelRequest(CancelRequest request);
        public Task<bool> AcceptRequest(AcceptRequest request);
    }
}
