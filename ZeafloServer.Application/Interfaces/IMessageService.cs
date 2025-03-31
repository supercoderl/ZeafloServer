using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Application.ViewModels.Messages;

namespace ZeafloServer.Application.Interfaces
{
    public interface IMessageService
    {
        public Task<PageResult<MessageViewModel>> GetAllMessagesAsync(
            PageQuery query,
            ActionStatus status,
            string searchTerm = "",
            SortQuery? sortQuery = null
        );
        public Task<bool> UpdateUnreadMessagesAsync(UpdateUnreadMessagesRequest request);
    }
}
