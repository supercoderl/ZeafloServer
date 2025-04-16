using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.PhotoPosts;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.Interfaces
{
    public interface IPhotoPostService
    {
        public Task<Guid> CreatePhotoPostAsync(CreatePhotoPostRequest request);
        public Task<PageResult<PhotoPostViewModel>> GetAllPhotoPostsAsync(
            PageQuery query,
            ActionStatus status,
            string scope = "others",
            string searchTerm = "",
            Guid? userId = null,
            SortQuery? sortQuery = null
        );
        public Task<List<StorageViewModel>> GetStorageAsync(Guid userId);
    }
}
