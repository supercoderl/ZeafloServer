using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.TripDurations;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.Interfaces
{
    public interface ITripDurationService
    {
        public Task<PageResult<TripDurationViewModel>> GetAllTripDurationsAsync(
            PageQuery query,
            ActionStatus status,
            string searchTerm = "",
            SortQuery? sortQuery = null
        );
        public Task<Guid> CreateTripDurationAsync(CreateTripDurationRequest request);
    }
}
