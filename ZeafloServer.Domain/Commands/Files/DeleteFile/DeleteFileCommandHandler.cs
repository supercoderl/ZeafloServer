using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Notifications;

namespace ZeafloServer.Domain.Commands.Files.DeleteFile
{
    public sealed class DeleteFileCommandHandler : CommandHandlerBase<bool>, IRequestHandler<DeleteFileCommand, bool>
    {
        private readonly Cloudinary _cloudinary;

        public DeleteFileCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            Cloudinary cloudinary
        ) : base(bus, unitOfWork, notifications)
        {
            _cloudinary = cloudinary;
        }

        public async Task<bool> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return false;

            var deleteParams = new DeletionParams(request.PublicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            if(result == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"Delete failed.",
                    ErrorCodes.ErrorInDeleting
                ));
                return false;
            }

            return result.Result == "ok";
        }
    }
}
