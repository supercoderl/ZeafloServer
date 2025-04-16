using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Helpers;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Shared.Events;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Commands.Points.AddPoint;

namespace ZeafloServer.Domain.Consumers
{
    public sealed class GainPointConsumer : IConsumer<GainPointMessageEvent>
    {
        private readonly ILogger<GainPointMessageEvent> _logger;
        private readonly IMediatorHandler _bus;

        public GainPointConsumer(
            ILogger<GainPointMessageEvent> logger,
            IMediatorHandler bus
        )
        {
            _logger = logger;
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<GainPointMessageEvent> context)
        {
            _logger.LogInformation("GainPointConsumer: {GainPointMessageEvent}", context.Message);
            Guid userId = context.Message.UserId;
            ActionType actionType = (ActionType)context.Message.ActionType;

            try
            {
                await _bus.SendCommandAsync(new AddPointCommand(
                    userId,
                    actionType
                ));

                _logger.LogInformation($"✅ Point has been gained for User {userId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Error gain point: {ex.Message}");
            }
        }
    }
}
