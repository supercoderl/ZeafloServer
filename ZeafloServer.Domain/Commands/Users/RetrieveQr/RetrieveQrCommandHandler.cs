using MediatR;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Commands.Users.UpdateUser;
using ZeafloServer.Domain.Errors;
using ZeafloServer.Domain.Helpers;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;

namespace ZeafloServer.Domain.Commands.Users.RetrieveQr
{
    public sealed class RetrieveQrCommandHandler : CommandHandlerBase<string>, IRequestHandler<RetrieveQrCommand, string>
    {
        private readonly IProcessingRepository _processingRepository;
        private readonly IUserRepository _userRepository;
        private readonly QRCodeHelpers _qRCodeHelpers;
        private readonly UploadHelpers _uploadHelpers;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public RetrieveQrCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IProcessingRepository processingRepository,
            IUserRepository userRepository,
            QRCodeHelpers qRCodeHelpers,
            UploadHelpers uploadHelpers
        ) : base(bus, unitOfWork, notifications) 
        {
            _processingRepository = processingRepository;
            _userRepository = userRepository;
            _qRCodeHelpers = qRCodeHelpers;
            _uploadHelpers = uploadHelpers;
        }

        [SupportedOSPlatform("windows")]
        public async Task<string> Handle(RetrieveQrCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return string.Empty;

            var isAnyProcess = await _processingRepository.CheckIsAnyQr(request.UserId);

            if(isAnyProcess != null)
            {
                if(isAnyProcess.Status == Enums.ProcessStatus.Completed)
                {
                    return await GetQrUrlFromUser(request.UserId);
                }
                else
                {
                    return "Your qr code is being generated, please wait a few minutes.";
                }
            }

            return await UploadQr(await GenerateQr(request.UserId), request.UserId, request.MessageType);
        }

        [SupportedOSPlatform("windows")]
        private async Task<Bitmap> GenerateQr(Guid userId)
        {
            // Create QR Code as bitmap
            Bitmap qrBitmap = _qRCodeHelpers.GenerateQRCode(userId.ToString());
            return await _qRCodeHelpers.AddLogoToQRCode(qrBitmap, "https://res.cloudinary.com/do02vtlo0/image/upload/v1742868116/a4wfcefzxexk5wq1sdyg.png");
        }

        [SupportedOSPlatform("windows")]
        private async Task<string> UploadQr(Bitmap qrBitmap, Guid userId, string messageType)
        {
            string qrUrl = await _uploadHelpers.UploadImageAsync(
                qrBitmap,
                userId.ToString(),
                "qrs"
            );

            var user = await _userRepository.GetByIdAsync(userId);

            if(user == null)
            {
                await NotifyAsync(new DomainNotification(
                    messageType,
                    $"There is no any user with id {userId}.",
                    ErrorCodes.ObjectNotFound
                ));

                return string.Empty;
            }

            user.SetQrUrl(qrUrl);
            _userRepository.Update(user);

            var process = new Entities.Processing(
                Guid.NewGuid(), 
                userId, 
                "Upload", 
                Enums.ProcessStatus.Completed, 
                TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone),
                null
            );

            _processingRepository.Add(process); 

            if (!await CommitAsync()) return string.Empty;

            return qrUrl;
        }

        private async Task<string> GetQrUrlFromUser(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            return user?.QrUrl ?? string.Empty;
        }
    }
}
