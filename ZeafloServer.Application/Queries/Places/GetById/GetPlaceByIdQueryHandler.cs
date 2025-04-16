using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Application.ViewModels.Places;
using Microsoft.EntityFrameworkCore;

namespace ZeafloServer.Application.Queries.Places.GetById
{
    public sealed record GetPlaceByIdQueryHandler : IRequestHandler<GetPlaceByIdQuery, PlaceViewModel?>
    {
        private readonly IMediatorHandler _bus;
        private readonly IPlaceRepository _placeRepository;

        public GetPlaceByIdQueryHandler(
            IMediatorHandler bus,
            IPlaceRepository placeRepository
        )
        {
            _bus = bus;
            _placeRepository = placeRepository;
        }

        public async Task<PlaceViewModel?> Handle(GetPlaceByIdQuery request, CancellationToken cancellationToken)
        {
            var place = await _placeRepository.GetByIdAsync(request.placeId, query =>
                query
                    .Include(pi => pi.PlaceImages)
                    .Include(pl => pl.PlaceLikes)
            ); //Missing ThenInclude

            if (place == null)
            {
                await _bus.RaiseEventAsync(new DomainNotification(
                    nameof(GetPlaceByIdQuery),
                    $"Place with id {request.placeId} could not be found.",
                    ErrorCodes.ObjectNotFound
                ));

                return null;
            }

            return PlaceViewModel.FromPlace(
                place
            );
        }
    }
}
