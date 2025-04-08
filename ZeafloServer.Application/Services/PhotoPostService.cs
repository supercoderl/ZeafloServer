using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.Queries.PhotoPosts.GetAll;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.PhotoPosts;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Commands.PhotoPosts.CreatePhotoPost;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Application.Services
{
    public class PhotoPostService : IPhotoPostService
    {
        private readonly IMediatorHandler _bus;

        public PhotoPostService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<Guid> CreatePhotoPostAsync(CreatePhotoPostRequest request)
        {
            AnnotationType? annotationType = null;

            if(request.AnnotationType != null && request.AnnotationValue != null)
            {
                if (Enum.TryParse<AnnotationType>(request.AnnotationType, true, out var parsedType))
                {
                    annotationType = parsedType;
                }
            }

            return await _bus.SendCommandAsync(new CreatePhotoPostCommand(
                Guid.NewGuid(),
                request.UserId,
                request.Image,
                annotationType,
                request.AnnotationValue
            ));
        }

        public async Task<PageResult<PhotoPostViewModel>> GetAllPhotoPostsAsync(PageQuery query, ActionStatus status, string scope = "others", string searchTerm = "", Guid? userId = null, SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllPhotoPostsQuery(query, status, scope, searchTerm, userId, sortQuery));
        }
    }
}
