using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.Queries.Messages.GetAll;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Messages;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Commands.Messages.UpdateUnreadMessage;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMediatorHandler _bus;

        public MessageService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<PageResult<MessageViewModel>> GetAllMessagesAsync(PageQuery query, ActionStatus status, string searchTerm = "", SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllMessagesQuery(query, status, searchTerm, sortQuery));
        }

        public async Task<bool> UpdateUnreadMessagesAsync(UpdateUnreadMessagesRequest request)
        {
            return await _bus.SendCommandAsync(new UpdateUnreadMessageCommand(request.SenderId, request.ReceiverId));
        }
    }
}
