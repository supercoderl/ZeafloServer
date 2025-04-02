using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Commands.Processes.CreateProcess;
using ZeafloServer.Domain.Commands.Processes.UpdateProcess;
using ZeafloServer.Domain.Commands.Users.UpdateUser;
using ZeafloServer.Domain.Helpers;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Shared.Events;

namespace ZeafloServer.Domain.Consumers
{
    public sealed class UploadConsumer : IConsumer<UploadMessageEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly UploadHelpers _uploadHelpers;
        private readonly ILogger<UploadConsumer> _logger;
        private readonly IMediatorHandler _bus;
        private readonly IUserRepository _userRepository;
        private readonly IProcessingRepository _processingRepository;
        private readonly QRCodeHelpers _qRCodeHelpers;
        private readonly ProcessingHelpers _processingHelpers;

        public UploadConsumer(
            IPublishEndpoint publishEndpoint,
            UploadHelpers uploadHelpers,
            ILogger<UploadConsumer> logger,
            IMediatorHandler bus,
            IUserRepository userRepository,
            IProcessingRepository processingRepository,
            QRCodeHelpers qRCodeHelpers,
            ProcessingHelpers processingHelpers
        )
        {
            _publishEndpoint = publishEndpoint;
            _uploadHelpers = uploadHelpers;
            _logger = logger;
            _bus = bus;
            _userRepository = userRepository;
            _processingRepository = processingRepository;
            _qRCodeHelpers = qRCodeHelpers;
            _processingHelpers = processingHelpers;
        }

        [SupportedOSPlatform("windows")]
        public async Task Consume(ConsumeContext<UploadMessageEvent> context)
        {
            _logger.LogInformation("UploadConsumer: {UploadMessageEvent}", context.Message);
            Guid userId = context.Message.UserId;
            string qrBase64 = context.Message.QrBase64;
            Entities.Processing? existingQr = null;

            try
            {
                existingQr = await _processingRepository.GetByUser(userId, "Generate");

                if (existingQr != null && existingQr.Status == Enums.ProcessStatus.Completed)
                {
                    _logger.LogInformation($"🔄 QR Code already uploaded for user {userId}, skipping upload.");
                    return;
                }

                if (existingQr == null)
                {
                    existingQr = await _processingHelpers.StartProcess(_bus, userId, "Upload");
                }
                else
                {
                    existingQr = await _processingHelpers.UpdateProcess(_bus, existingQr, Enums.ProcessStatus.InProgress);
                }

                string qrUrl = await _uploadHelpers.UploadImageAsync(
                    _qRCodeHelpers.ConvertBase64ToBitmap(qrBase64), 
                    userId.ToString(), 
                    "qrs"
                );

                if (existingQr != null)
                {
                    existingQr = await _processingHelpers.UpdateProcess(_bus, existingQr, Enums.ProcessStatus.Completed);
                }

                var user = await _userRepository.GetByIdAsync(userId);

                if (user != null)
                {
                    await _bus.SendCommandAsync(new UpdateUserCommand(
                        user.UserId,
                        user.Username,
                        user.Email,
                        user.Fullname,
                        user.Bio,
                        user.AvatarUrl,
                        user.CoverPhotoUrl,
                        user.PhoneNumber,
                        user.Website,
                        user.Location,
                        qrUrl,
                        user.Gender,
                        user.IsOnline,
                        user.LastLoginTime
                    ));
                }

                _logger.LogInformation($"✅ QR Code uploaded for User {userId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Error uploading QR Code: {ex.Message}");
                if (existingQr != null)
                {
                    await _processingHelpers.UpdateProcess(_bus, existingQr, Enums.ProcessStatus.Failed);
                }
            }
        }
    }
}
