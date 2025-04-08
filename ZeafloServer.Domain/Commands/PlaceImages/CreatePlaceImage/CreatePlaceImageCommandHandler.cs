using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Shared.Events.PlaceImage;

namespace ZeafloServer.Domain.Commands.PlaceImages.CreatePlaceImage
{
    public sealed class CreatePlaceImageCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<CreatePlaceImageCommand, Guid>
    {
        private readonly IPlaceImageRepository _placeImageRepository;

        public CreatePlaceImageCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IPlaceImageRepository placeImageRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _placeImageRepository = placeImageRepository;
        }

        public async Task<Guid> Handle(CreatePlaceImageCommand request, CancellationToken cancellationToken)
        {
            if(!await TestValidityAsync(request)) return Guid.Empty;

            var placeImage = new Entities.PlaceImage(
                request.PlaceImageId,
                request.PlaceId,
                request.ImageUrl
            );

            _placeImageRepository.Add( placeImage );

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new PlaceImageCreatedEvent(placeImage.PlaceImageId));
            }

            return placeImage.PlaceImageId;
        }
    }
}
