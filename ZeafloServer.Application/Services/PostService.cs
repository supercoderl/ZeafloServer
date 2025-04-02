using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.Queries.Posts.GetAll;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Posts;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Commands.Posts.CreatePost;
using ZeafloServer.Domain.Commands.Posts.ReactPost;
using ZeafloServer.Domain.Commands.Posts.SavePost;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IMediatorHandler _bus;

        public PostService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<Guid> CreatePostAsync(CreatePostRequest request)
        {
            return await _bus.SendCommandAsync(new CreatePostCommand(
                Guid.NewGuid(),
                request.UserId,
                request.Title,
                request.Content,
                request.ThumbnailUrl,
                request.Visibility
            ));
        }

        public async Task<PageResult<PostViewModel>> GetAllPostsAsync(PageQuery query, ActionStatus status, string searchTerm = "", SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllPostsQuery(query, status, searchTerm, sortQuery));   
        }

        public async Task<Guid> ReactPostAsync(ReactPostRequest request)
        {
            return await _bus.SendCommandAsync(new ReactPostCommand(
                Guid.NewGuid(),
                request.PostId,
                request.UserId,
                request.ReactionType
            ));
        }

        public async Task<Guid> SavePostAsync(SavePostRequest request)
        {
            return await _bus.SendCommandAsync(new SavePostCommand(
                Guid.NewGuid(),
                request.PostId,
                request.UserId
            ));
        }
    }
}
