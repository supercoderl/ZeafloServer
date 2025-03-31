using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.Queries.Comments.GetAll;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Comments;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMediatorHandler _bus;

        public CommentService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<PageResult<CommentViewModel>> GetAllCommentsAsync(PageQuery query, ActionStatus status, string searchTerm = "", SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllCommentsQuery(query, status, searchTerm, sortQuery));
        }
    }
}
