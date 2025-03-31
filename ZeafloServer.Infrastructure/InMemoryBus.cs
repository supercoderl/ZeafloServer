using MediatR;
using ZeafloServer.Domain.Commands;
using ZeafloServer.Domain.DomainEvents;
using ZeafloServer.Domain.EventHandler.Fanout;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Shared.Events;

namespace ZeafloServer.Infrastructure
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IDomainEventStore _domainEventStore;
        private readonly IFanoutEventHandler _fanoutEventHandler;

        public InMemoryBus(
            IMediator mediator,
            IDomainEventStore domainEventStore,
            IFanoutEventHandler fanoutEventHandler
        )
        {
            _mediator = mediator;
            _domainEventStore = domainEventStore;
            _fanoutEventHandler = fanoutEventHandler;
        }

        public Task<TResponse> QueryAsync<TResponse>(IRequest<TResponse> query)
        {
            return _mediator.Send(query);
        }

        public async Task RaiseEventAsync<T>(T @event) where T : DomainEvent
        {
            await _domainEventStore.SaveAsync(@event);

            await _mediator.Publish(@event);

            if (@event is DomainEvent domainEvent)
            {
                await _fanoutEventHandler.HandleDomainEventAsync(domainEvent);
            }
            else
            {
                throw new InvalidOperationException("Invalid event type.");
            }
        }

        public Task<TResponse> SendCommandAsync<TResponse>(CommandBase<TResponse> command)
        {
            return _mediator.Send(command);
        }
    }
}
