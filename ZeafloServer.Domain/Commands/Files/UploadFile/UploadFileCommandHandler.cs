using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Notifications;

namespace ZeafloServer.Domain.Commands.Files.UploadFile
{
    public sealed class UploadFileCommandHandler : CommandHandlerBase<string>, IRequestHandler<UploadFileCommand, string>
    {
        private readonly Cloudinary _cloudinary;

        public UploadFileCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            Cloudinary cloudinary
        ) : base( bus, unitOfWork, notifications )
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return string.Empty;

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(request.FileName, request.Base64String),
                Transformation = new Transformation().Quality("auto:best").FetchFormat("auto"),
                Folder = request.FileName
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if(uploadResult == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"Failed when uploading file to server.",
                    ErrorCodes.UploadFailed
                ));
                return string.Empty;
            }

            return uploadResult.SecureUrl.ToString();
        }
    }
}
