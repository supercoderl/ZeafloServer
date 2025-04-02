using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.Queries.Cities.GetAll;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Cities;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Commands.Cities.CreateCity;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Application.Services
{
    public class CityService : ICityService
    {
        private readonly IMediatorHandler _bus;

        public CityService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<Guid> CreateCityAsync(CreateCityRequest request)
        {
            return await _bus.SendCommandAsync(new CreateCityCommand(
                Guid.NewGuid(),
                request.Name,
                request.PostalCode,
                request.Latitude,
                request.Longitude
            ));
        }

        public async Task<PageResult<CityViewModel>> GetAllCitiesAsync(PageQuery query, ActionStatus status, string searchTerm = "", SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllCitiesQuery(query, status, searchTerm, sortQuery));
        }
    }
}
