﻿using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Constants;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Shared.Events;

namespace ZeafloServer.Domain.EventHandler.Fanout
{
    public sealed class FanoutEventHandler : IFanoutEventHandler
    {
        private readonly IPublishEndpoint _massTransit;
        private readonly IUser _user;

        public FanoutEventHandler(
            IPublishEndpoint massTransit, IUser user)
        {
            _massTransit = massTransit;
            _user = user;
        }

        public async Task<T> HandleDomainEventAsync<T>(T @event) where T : DomainEvent
        {
            var fanoutDomainEvent =
                new FanoutDomainEvent(
                    @event.AggregateId,
                    @event,
                    _user.GetUserId());

            await _massTransit.Publish(fanoutDomainEvent);
            await _massTransit.Publish(@event);

            return @event;
        }
    }
}
