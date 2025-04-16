using DocumentFormat.OpenXml.Spreadsheet;
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
using ZeafloServer.Shared.Events.User;

namespace ZeafloServer.Domain.Commands.Users.ChangeUserAvatar
{
    public sealed class ChangeUserAvatarCommandHandler : CommandHandlerBase<string>, IRequestHandler<ChangeUserAvatarCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly UploadHelpers _uploadHelpers;
        private readonly QRCodeHelpers _qRCodeHelpers;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public ChangeUserAvatarCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IUserRepository userRepository,
            UploadHelpers uploadHelpers,
            QRCodeHelpers qRCodeHelpers
        ) : base(bus, unitOfWork, notifications)
        {
            _userRepository = userRepository;
            _uploadHelpers = uploadHelpers;
            _qRCodeHelpers = qRCodeHelpers;
        }

        [SupportedOSPlatform("windows")]
        public async Task<string> Handle(ChangeUserAvatarCommand request, CancellationToken cancellationToken)
        {
            if(!await TestValidityAsync(request)) return string.Empty;

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if(user == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any user with Id {request.UserId}.",
                    ErrorCodes.ObjectNotFound
                ));
                return string.Empty;
            }

            var avatarString = await _uploadHelpers.UploadImageAsync(
                _qRCodeHelpers.ConvertBase64ToBitmap(request.AvatarBase64String),
                string.Concat(request.UserId, "_", TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone).ToString()),
                "avatars"
            );

            if (string.IsNullOrEmpty(avatarString))
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    "There was a problem while uploading the image.",
                    ErrorCodes.UploadFailed
                ));
                return string.Empty;
            }

            user.SetAvatarUrl(avatarString);
            _userRepository.Update(user);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new UserUpdatedEvent(request.UserId));
            }

            return avatarString;
        }
    }
}
