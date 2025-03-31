using MassTransit;
using Microsoft.Extensions.Logging;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Helpers;
using ZeafloServer.Shared.Events;

namespace ZeafloServer.Domain.Consumers
{
    public sealed class GenerateQRCodeConsumer : IConsumer<GenerateQRCodeMessageEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<GenerateQRCodeConsumer> _logger;
        private readonly QRCodeHelpers _qRCodeHelpers;

        public GenerateQRCodeConsumer(
            IPublishEndpoint publishEndpoint,
            ILogger<GenerateQRCodeConsumer> logger,
            QRCodeHelpers qRCodeHelpers
        )
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
            _qRCodeHelpers = qRCodeHelpers;
        }

        [SupportedOSPlatform("windows")]
        public async Task Consume(ConsumeContext<GenerateQRCodeMessageEvent> context)
        {
            _logger.LogInformation("GenerateQRCodeConsumer: {GenerateQRCodeMessageEvent}", context.Message);
            var userId = context.Message.UserId;

            try
            {
                // Create QR Code as bitmap
                Bitmap qrBitmap = _qRCodeHelpers.GenerateQRCode(userId.ToString());
                qrBitmap = await _qRCodeHelpers.AddLogoToQRCode(qrBitmap, "https://res.cloudinary.com/do02vtlo0/image/upload/v1742868116/a4wfcefzxexk5wq1sdyg.png");

                // Send event after created QR Code successfully
                await _publishEndpoint.Publish(new UploadMessageEvent
                (
                    userId,
                    _qRCodeHelpers.ConvertBitmapToBase64(qrBitmap)
                ));

                _logger.LogInformation($"✅ QR Code created for user {userId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Error generating QR Code: {ex.Message}");
            }
        }
    }
}
