using Google.Protobuf.WellKnownTypes;
using MassTransit.Initializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Common;
using ZeafloServer.Application.ViewModels.FriendShips;
using ZeafloServer.Application.ViewModels.Messages;
using ZeafloServer.Domain.Commands.FriendShips.AddFriend;
using ZeafloServer.Domain.Commands.Users.UpdateUser;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;

namespace ZeafloServer.Application.Hubs
{
    [Authorize]
    public sealed class ChatHub : Hub
    {
        private readonly static Dictionary<string, string> _connections = new Dictionary<string, string>();
        private readonly static object _lock = new object();
        private readonly IUser _user;
        private readonly IUserRepository _userRepository;
        private readonly IFriendShipRepository _friendShipRepository;
        private readonly IMediatorHandler _bus;
        const string defaultGroup = "Zeaflo Chat Room";
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public ChatHub(
            IUser user,
            IUserRepository userRepository,
            IFriendShipRepository friendShipRepository,
            IMediatorHandler bus
        )
        {
            _user = user;
            _userRepository = userRepository;
            _friendShipRepository = friendShipRepository;
            _bus = bus;
        }

        public override async Task<Task> OnConnectedAsync()
        {
            try
            {
                await AddOnline();
                //Clients.Caller.SendAsync("getProfileInfo", "test", "test");
                await Groups.AddToGroupAsync(Context.ConnectionId, defaultGroup);
/*                GetActiveGroupUser();*/
                var userId = CurrentUserId;
                lock (_lock)
                {

                    if (_connections.Keys.FirstOrDefault(t => t.Equals(userId)) == null)
                    {
                        _connections.Add(userId, Context.ConnectionId);
                    }
                    else
                    {
                        _connections[userId] = Context.ConnectionId;
                    }
                }
                return base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                await SendError(ex.Message);
                throw;
            }
        }

        public override async Task<Task> OnDisconnectedAsync(Exception? exception)
        {
            await RemoveOnline();
            _connections.Remove(CurrentUserId);
            return base.OnDisconnectedAsync(exception);
        }

        # region  user

        /// <summary>
        /// Join the private chat channel
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="friendId"></param>
        /// <returns></returns>
        public async Task JoinFriendSocket(string userId, string friendId)
        {
            var relation = await _friendShipRepository.GetByUserAndFriendAsync(Guid.Parse(userId), Guid.Parse(friendId));
            if (relation != null)
            {
                var roomName = string.Compare(userId, friendId) == 1 ? userId + friendId : friendId + userId;
                await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                await Clients.Caller.SendAsync("JoinFriendSocket", MessageResult.Instance.Ok("Entered private chat channel successfully", relation));
            }
        }

        /// <summary>
        /// Send a private message
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task FriendMessage(CreateMessageRequest request)
        {
            try
            {
                var userId = CurrentUserId;

                var message = await MessageCore.SendFriendMessage(Guid.Parse(userId), _userRepository, _friendShipRepository, request, _bus);
 
                if (message == null)
                {
                    throw new Exception("Failed to send message");
                }

                var roomName = string.Compare(userId, request.ReceiverId.ToString()) == 1 ? userId + request.ReceiverId : request.ReceiverId + userId;
                await Clients.Group(roomName).SendAsync("FriendMessage", MessageResult.Instance.Ok("", message));
            }
            catch (Exception ex)
            {
                await SendError(ex.Message);
            }
        }

        /// <summary>
        /// Add friends
        /// </summary>
        /// <param name="friendId"></param>
        /// <returns></returns>
        public async Task AddFriend(string friendId)
        {
            try
            {
                var userId = CurrentUserId;
                if (friendId.Equals(userId))
                {
                    throw new Exception("Cannot add myself as a friend");
                }

                var relation1 = await _friendShipRepository.GetByUserAndFriendAsync(Guid.Parse(userId), Guid.Parse(friendId));
                var relation2 = await _friendShipRepository.GetByUserAndFriendAsync(Guid.Parse(friendId), Guid.Parse(userId));

                var roomName = string.Compare(userId, friendId) == 1 ? userId + friendId : friendId + userId;
                if (relation1 != null || relation2 != null)
                {
                    throw new Exception("You are already friends, please do not add again");
                }

                var result = await _bus.SendCommandAsync(new AddFriendCommand(
                    Guid.NewGuid(),
                    Guid.Parse(userId),
                    Guid.Parse(friendId)
                ));

                await SendUserMessage("AddFriend", userId, $"Add friend successfully", null);
                await SendUserMessage("AddFriend", friendId, $" You have a new add friend request", null);
            }
            catch (Exception ex)
            {
                await SendError(ex.Message);
            }
        }

        /// <summary>
        /// Delete friends
        /// </summary>
        /// <param name="friendId"></param>
        /// <returns></returns>
        public async Task ExitFriend(string friendId)
        {
            var userId = CurrentUserId;
            var map1 = await _friendShipRepository.GetByUserAndFriendAsync(Guid.Parse(userId), Guid.Parse(friendId));
            var map2 = await _friendShipRepository.GetByUserAndFriendAsync(Guid.Parse(friendId), Guid.Parse(userId));

            if (map1 != null && map2 != null)
            {
/*                _dbContext.Remove(map1);
                _dbContext.Remove(map2);
                _dbContext.SaveChanges();
                await SendCallerMessage("ExitFriend", "Friend deleted successfully", map1);*/
            }
            else
            {
                await SendError("Failed to delete friend");
            }

        }

        # endregion

        /// <summary>
        /// Get all group and friend data
        /// </summary>
        /// <returns></returns>
        public async Task ChatData()
        {
            try
            {
                var userId = CurrentUserId;
                if (userId == null)
                {
                    return;
                }

/*                var groupList = _dbContext.ChatGroupMaps.Include(t => t.ChatGroup).ThenInclude(t => t.ChatGroupMessages).ThenInclude(t => t.ChatUser).Where(t => t.UserId == userId).Select(t => t.ChatGroup).ToList();
                var groupDtoList = _mapper.Map<List<ChatGroup>, List<GroupDto>>(groupList);*/

                var friendMaps = await _friendShipRepository.GetListByUserAsync(Guid.Parse(userId));

                var friends = friendMaps.Select(map =>
                {
                    var friendId = map.UserId == Guid.Parse(userId) ? map.FriendId : map.UserId;

                    var messages = (map.Friend?.SenderMessages ?? Enumerable.Empty<Message>())
                        .Concat(map.Friend?.ReceiverMessages ?? Enumerable.Empty<Message>())
                        .Where(m => (m.SenderId == friendId && m.ReceiverId == Guid.Parse(userId)) ||
                                    (m.SenderId == Guid.Parse(userId) && m.ReceiverId == friendId)) // Just get messages both 2 people
                        .OrderByDescending(m => m.CreatedAt)
                        .Take(20)
                        .Select(m => MessageViewModel.FromMessage(m))
                        .ToList();

                    return FriendShipViewModel.FromFriendShip(
                        map,
                        map.UserId == Guid.Parse(userId) ? map.UserId : map.FriendId,
                        new ContactInfo
                        {
                            FriendId = friendId,
                            Username = map.UserId == Guid.Parse(userId)
                                ? (map.Friend?.Username ?? string.Empty)
                                : (map.User?.Username ?? string.Empty),
                            Fullname = map.UserId == Guid.Parse(userId)
                                ? (map.Friend?.Fullname ?? string.Empty)
                                : (map.User?.Fullname ?? string.Empty),
                            AvatarUrl = map.UserId == Guid.Parse(userId)
                                ? (map.Friend?.AvatarUrl ?? string.Empty)
                                : (map.User?.AvatarUrl ?? string.Empty),
                            Status = map.Status
                        },
                        messages
                    );
                }).ToList();

/*                List<ChatUsersDto> userList = new List<ChatUsersDto>();
                foreach (var groupMessages in groupList.Select(t => t.ChatGroupMessages))
                {
                    foreach (var item in groupMessages)
                    {
                        if (userList.FirstOrDefault(t => t.UserId == item.UserId) == null)
                        {
                            userList.Add(_mapper.Map<ChatUser, ChatUsersDto>(item.ChatUser));
                        }
                    }
                }
                userList.AddRange(_mapper.Map<List<FriendDto>, List<ChatUsersDto>>(frindDtoList));*/

                await Clients.Caller.SendAsync("ChatData", MessageResult.Instance.Ok("Successfully obtained chat data", new
                {
                    friendData = friends
                }));

            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task SendError(string message)
        {
            await Clients.Caller.SendAsync("OnError", MessageResult.Instance.Error(message, null));
        }
        private async Task SendCallerMessage(string method, string message, object data)
        {
            await Clients.Caller.SendAsync(method, MessageResult.Instance.Ok(message, data));
        }
        private async Task SendUserMessage(string method, string userId, string message, object? data)
        {
            if (_connections.Keys.FirstOrDefault(t => t.Equals(userId)) != null)
            {
                await Clients.Client(_connections[userId]).SendAsync(method, MessageResult.Instance.Ok(message, data));
            }
        }

        public string CurrentUserId
        {
            get
            {
                return _user.GetUserId().ToString();
            }
        }

        private async Task AddOnline()
        {
            string userId = CurrentUserId.ToString();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return;
            }

            var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));

            if(user != null)
            {
                await _bus.SendCommandAsync(new UpdateUserCommand(
                    user.UserId,
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
                    user.Gender,
                    true,
                    TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone)
                ));
            }
        }

        private async Task RemoveOnline()
        {
            string userId = CurrentUserId.ToString();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return;
            }

            var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));

            if (user != null)
            {
                await _bus.SendCommandAsync(new UpdateUserCommand(
                    user.UserId,
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
                    user.Gender,
                    false,
                    user.LastLoginTime
                ));
            }
        }
    }
}
