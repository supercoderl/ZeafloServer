using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Shared.Events.TripDuration;

namespace ZeafloServer.Domain.Commands.TripDurations.CreateTripDuration
{
    public sealed class CreateTripDurationCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<CreateTripDurationCommand, Guid>
    {
        private readonly ITripDurationRepository _tripDurationRepository;

        public CreateTripDurationCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ITripDurationRepository tripDurationRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _tripDurationRepository = tripDurationRepository;
        }

        public async Task<Guid> Handle(CreateTripDurationCommand request, CancellationToken cancellationToken)
        {
            if(!await TestValidityAsync(request)) return Guid.Empty;

            var tripDuration = new Entities.TripDuration(
                request.TripDurationId,
                request.Label,
                request.Days,
                request.Nights
            );

            _tripDurationRepository.Add(tripDuration);  

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new TripDurationCreatedEvent(tripDuration.TripDurationId));
            }

            return tripDuration.TripDurationId;
        }
    }
}
