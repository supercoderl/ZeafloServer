using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;
using ZeafloServer.Domain.Helpers;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Shared.Events.PhotoPost;

namespace ZeafloServer.Domain.Commands.PhotoPosts.CreatePhotoPost
{
    public sealed class CreatePhotoPostCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<CreatePhotoPostCommand, Guid>
    {
        private readonly IPhotoPostRepository _photoPostRepository;
        private readonly UploadHelpers _uploadHelpers;
        private readonly QRCodeHelpers _qRCodeHelpers;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public CreatePhotoPostCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IPhotoPostRepository photoPostRepository,
            UploadHelpers uploadHelpers,
            QRCodeHelpers qRCodeHelpers
        ) : base(bus, unitOfWork, notifications)
        {
            _photoPostRepository = photoPostRepository;
            _uploadHelpers = uploadHelpers;
            _qRCodeHelpers = qRCodeHelpers;
        }

        [SupportedOSPlatform("windows")]
        public async Task<Guid> Handle(CreatePhotoPostCommand request, CancellationToken cancellationToken)
        {
            if(!await TestValidityAsync(request)) return Guid.Empty;

            DateTime now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);
            var imageUrl = await _uploadHelpers.UploadImageAsync(
                _qRCodeHelpers.ConvertBase64ToBitmap(request.Image),
                string.Concat(request.UserId, "_", now.ToString()),
                "photoPosts"
            );

            if(string.IsNullOrWhiteSpace(imageUrl))
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    "Image upload failed.",
                    ErrorCodes.UploadFailed
                ));
                return Guid.Empty;
            }

            var photoPost = new Entities.PhotoPost(
                request.PhotoPostId,
                request.UserId,
                imageUrl,
                request.AnnotationType,
                request.AnnotationValue,
                now
            );

            _photoPostRepository.Add(photoPost);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new PhotoPostCreatedEvent(photoPost.PhotoPostId));
            }

            return photoPost.PhotoPostId;
        }
    }
}
