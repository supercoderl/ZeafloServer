using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.Queries.Places.GetAll;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Places;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Commands.Places.CreatePlace;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Application.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly IMediatorHandler _bus;

        public PlaceService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<Guid> CreatePlaceAsync(CreatePlaceRequest request)
        {
            return await _bus.SendCommandAsync(new CreatePlaceCommand(
                Guid.NewGuid(),
                request.Name,
                request.Type,
                request.CityId,
                request.Latitude,
                request.Logitude,
                request.Rating,
                request.ReviewCount,
                request.IsOpen
            ));
        }

        public async Task<PageResult<PlaceViewModel>> GetAllPlacesAsync(PageQuery query, ActionStatus status, string searchTerm = "", SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllPlacesQuery(query, status, searchTerm, sortQuery));  
        }
    }
}
