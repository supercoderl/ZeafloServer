using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Comments;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.Interfaces
{
    public interface ICommentService
    {
        public Task<PageResult<CommentViewModel>> GetAllCommentsAsync(
            PageQuery query,
            ActionStatus status,
            string searchTerm = "",
            SortQuery? sortQuery = null
        );
    }
}
