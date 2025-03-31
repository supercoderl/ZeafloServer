using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.PostMedias;

namespace ZeafloServer.Application.Interfaces
{
    public interface IPostMediaService
    {
        public Task<Guid> CreatePostMediaAsync(CreatePostMediaRequest request);
    }
}
