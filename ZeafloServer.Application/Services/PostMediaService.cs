using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.ViewModels.PostMedias;
using ZeafloServer.Domain.Commands.PostMedias.CreatePostMedia;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Application.Services
{
    public class PostMediaService : IPostMediaService
    {
        private readonly IMediatorHandler _bus;

        public PostMediaService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<Guid> CreatePostMediaAsync(CreatePostMediaRequest request)
        {
            return await _bus.SendCommandAsync(new CreatePostMediaCommand(
                Guid.NewGuid(),
                request.PostId,
                request.MediaUrl,
                request.MediaType
            ));
        }
    }
}
