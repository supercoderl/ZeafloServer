using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Shared.Events.Place;

namespace ZeafloServer.Domain.Commands.Places.CreatePlace
{
    public sealed class CreatePlaceCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<CreatePlaceCommand, Guid>
    {
        private readonly IPlaceRepository _placeRepository;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public CreatePlaceCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IPlaceRepository placeRepository
        ) : base(bus, unitOfWork, notifications )   
        {
            _placeRepository = placeRepository;
        }

        public async Task<Guid> Handle(CreatePlaceCommand request, CancellationToken cancellationToken)
        {
            if(!await TestValidityAsync(request)) return Guid.Empty;

            var place = new Entities.Place(
                request.PlaceId,
                request.Name,
                request.Address,
                request.Type,
                request.CityId,
                request.Latitude,
                request.Longitude,
                request.Rating,
                request.ReviewCount,
                request.IsOpen,
                TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone),
                null
            );

            _placeRepository.Add(place);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new PlaceCreatedEvent(place.PlaceId));
            }

            return place.PlaceId;
        }
    }
}
