using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Shared.Events.City;

namespace ZeafloServer.Domain.Commands.Cities.CreateCity
{
    public sealed class CreateCityCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<CreateCityCommand, Guid>
    {
        private readonly ICityRepository _cityRepository;

        public CreateCityCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ICityRepository cityRepository
        ) : base(bus, unitOfWork, notifications )
        {
            _cityRepository = cityRepository;
        }

        public async Task<Guid> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return Guid.Empty;

            var city = new Entities.City(
                request.CityId,
                request.Name,
                request.PostalCode,
                request.Latitude,
                request.Longitude
            );

            _cityRepository.Add( city );

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new CityCreatedEvent(city.CityId));
            }

            return city.CityId;
        }
    }
}
