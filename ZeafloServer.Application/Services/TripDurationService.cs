using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.Queries.TripDurations.GetAll;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels.TripDurations;
using ZeafloServer.Domain.Commands.TripDurations.CreateTripDuration;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Application.Services
{
    public class TripDurationService : ITripDurationService
    {
        private readonly IMediatorHandler _bus;

        public TripDurationService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<Guid> CreateTripDurationAsync(CreateTripDurationRequest request)
        {
            return await _bus.SendCommandAsync(new CreateTripDurationCommand(
                Guid.NewGuid(),
                request.Label,
                request.Days,
                request.Nights
            ));
        }

        public async Task<PageResult<TripDurationViewModel>> GetAllTripDurationsAsync(PageQuery query, ActionStatus status, string searchTerm = "", SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllTripDurationsQuery(query, status, searchTerm, sortQuery));
        }
    }
}
