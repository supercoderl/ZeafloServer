using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Commands.Processes.CreateProcess;
using ZeafloServer.Domain.Commands.Processes.UpdateProcess;
using ZeafloServer.Domain.Helpers;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Shared.Events;

namespace ZeafloServer.Domain.Consumers
{
    public sealed class GenerateQRCodeConsumer : IConsumer<GenerateQRCodeMessageEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<GenerateQRCodeConsumer> _logger;
        private readonly QRCodeHelpers _qRCodeHelpers;
        private readonly ProcessingHelpers _processingHelpers;
        private readonly IProcessingRepository _processingRepository;
        private readonly IMediatorHandler _bus;

        public GenerateQRCodeConsumer(
            IPublishEndpoint publishEndpoint,
            ILogger<GenerateQRCodeConsumer> logger,
            QRCodeHelpers qRCodeHelpers,
            ProcessingHelpers processingHelpers,
            IProcessingRepository processingRepository,
            IMediatorHandler bus
        )
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
            _qRCodeHelpers = qRCodeHelpers;
            _processingHelpers = processingHelpers;
            _processingRepository = processingRepository;
            _bus = bus;
        }

        [SupportedOSPlatform("windows")]
        public async Task Consume(ConsumeContext<GenerateQRCodeMessageEvent> context)
        {
            _logger.LogInformation("GenerateQRCodeConsumer: {GenerateQRCodeMessageEvent}", context.Message);
            var userId = context.Message.UserId;
            Entities.Processing? existingQr = null;

            try
            {
                existingQr = await _processingRepository.GetByUser(userId, "Generate");

                if (existingQr != null && existingQr.Status == Enums.ProcessStatus.Completed)
                {
                    _logger.LogInformation($"🔄 QR Code already generated for user {userId}, skipping generation.");
                    return;
                }

                if (existingQr == null)
                {
                    existingQr = await _processingHelpers.StartProcess(_bus, userId, "Generate");
                }
                else
                {
                    existingQr = await _processingHelpers.UpdateProcess(_bus, existingQr, Enums.ProcessStatus.InProgress);
                }

                // Create QR Code as bitmap
                Bitmap qrBitmap = _qRCodeHelpers.GenerateQRCode(userId.ToString());
                qrBitmap = await _qRCodeHelpers.AddLogoToQRCode(qrBitmap, "https://res.cloudinary.com/do02vtlo0/image/upload/v1742868116/a4wfcefzxexk5wq1sdyg.png");

                if (existingQr != null)
                {
                    existingQr = await _processingHelpers.UpdateProcess(_bus, existingQr, Enums.ProcessStatus.Completed);
                }

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
                if (existingQr != null)
                {
                    await _processingHelpers.UpdateProcess(_bus, existingQr, Enums.ProcessStatus.Failed);
                }
            }
        }
    }
}
