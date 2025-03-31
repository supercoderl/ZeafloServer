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
        private readonly QRCodeHelpers _qRCodeHelpers;

        public UploadConsumer(
            IPublishEndpoint publishEndpoint,
            UploadHelpers uploadHelpers,
            ILogger<UploadConsumer> logger,
            IMediatorHandler bus,
            IUserRepository userRepository,
            QRCodeHelpers qRCodeHelpers
        )
        {
            _publishEndpoint = publishEndpoint;
            _uploadHelpers = uploadHelpers;
            _logger = logger;
            _bus = bus;
            _userRepository = userRepository;
            _qRCodeHelpers = qRCodeHelpers;
        }

        [SupportedOSPlatform("windows")]
        public async Task Consume(ConsumeContext<UploadMessageEvent> context)
        {
            _logger.LogInformation("UploadConsumer: {UploadMessageEvent}", context.Message);
            Guid userId = context.Message.UserId;
            string qrBase64 = context.Message.QrBase64;

            try
            {
                string qrUrl = await _uploadHelpers.UploadImageAsync(
                    _qRCodeHelpers.ConvertBase64ToBitmap(qrBase64), 
                    userId.ToString(), 
                    "qrs"
                );

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
            }
        }
    }
}
