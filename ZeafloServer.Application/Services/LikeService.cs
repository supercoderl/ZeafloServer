using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.Queries.Likes.GetAll;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Likes;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Application.Services
{
    public class LikeService : ILikeService
    {
        private readonly IMediatorHandler _bus;

        public LikeService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<PageResult<LikeViewModel>> GetAllLikesAsync(PageQuery query, ActionStatus status, string searchTerm = "", SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllLikesQuery(query, status, searchTerm, sortQuery));
        }
    }
}
