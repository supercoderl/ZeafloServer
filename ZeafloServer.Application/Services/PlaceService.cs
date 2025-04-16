using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.Queries.Places.GetAll;
using ZeafloServer.Application.Queries.Places.GetById;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Places;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Commands.Places.CreatePlace;
using ZeafloServer.Domain.Commands.Places.ImportPlace;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Errors;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Notifications;

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
                request.Address,
                request.Description,
                request.Type,
                request.CityId,
                request.Latitude,
                request.Logitude,
                request.Rating,
                request.ReviewCount,
                request.IsOpen
            ));
        }

        public async Task<PageResult<PlaceViewModel>> GetAllPlacesAsync(PageQuery query, ActionStatus status, List<PlaceType> types, string searchTerm = "", SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllPlacesQuery(query, status, types, searchTerm, sortQuery));  
        }

        public async Task<PlaceViewModel?> GetPlaceByIdAsync(Guid placeId)
        {
            return await _bus.QueryAsync(new GetPlaceByIdQuery(placeId));
        }

        public async Task<List<Guid>> ImportPlacesAsync(ImportPlaceRequest request)
        {
            using var stream = new MemoryStream();
            await request.File.CopyToAsync(stream);


            Dictionary<string, string>? headers = null;
            try
            {
                headers = JsonSerializer.Deserialize<Dictionary<string, string>>(request.HeadersJSON);
            }
            catch
            {
                await _bus.RaiseEventAsync(new DomainNotification(
                    "ImportPlace",
                    "Invalid headers JSON.",
                    ErrorCodes.InvalidFile
                ));
            }

            if (headers == null)
            {
                await _bus.RaiseEventAsync(new DomainNotification(
                    "ImportPlace",
                    "Invalid headers JSON.",
                    ErrorCodes.InvalidFile
                ));
                return new List<Guid>();    
            }

            return await _bus.SendCommandAsync(new ImportPlaceCommand(
                stream,
                headers
            ));
        }
    }
}
