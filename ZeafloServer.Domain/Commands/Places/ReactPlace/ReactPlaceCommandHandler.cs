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

namespace ZeafloServer.Domain.Commands.Places.ReactPlace
{
    public sealed class ReactPlaceCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<ReactPlaceCommand, Guid>
    {
        private readonly IPlaceLikeRepository _placeLikeRepository;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public ReactPlaceCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notification,
            IPlaceLikeRepository placeLikeRepository
        ) : base(bus, unitOfWork, notification)
        {
            _placeLikeRepository = placeLikeRepository;
        }

        public async Task<Guid> Handle(ReactPlaceCommand request, CancellationToken cancellationToken)
        {
            if(!await TestValidityAsync(request)) return Guid.Empty;

            var placeLike = new Entities.PlaceLike(
                request.PlaceLikeId,
                request.PlaceId,
                request.UserId,
                TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone)
            );

            _placeLikeRepository.Add( placeLike );

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new PlaceReactedEvent(placeLike.PlaceLikeId));
            }

            return placeLike.PlaceLikeId;
        }
    }
}
