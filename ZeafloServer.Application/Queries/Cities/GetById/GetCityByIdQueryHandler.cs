using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Cities;
using ZeafloServer.Domain.Errors;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;

namespace ZeafloServer.Application.Queries.Cities.GetById
{
    public sealed class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, CityViewModel?>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMediatorHandler _bus;

        public GetCityByIdQueryHandler(
            ICityRepository cityRepository,
            IMediatorHandler bus
        )
        {
            _cityRepository = cityRepository;
            _bus = bus;
        }

        public async Task<CityViewModel?> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            var city = await _cityRepository.GetByIdAsync( request.Id );

            if (city == null)
            {
                await _bus.RaiseEventAsync(new DomainNotification(
                    nameof(GetCityByIdQuery),
                    $"City with id {request.Id} could not be found.",
                    ErrorCodes.ObjectNotFound
                ));

                return null;
            }

            return CityViewModel.FromCity(city);
        }
    }
}
