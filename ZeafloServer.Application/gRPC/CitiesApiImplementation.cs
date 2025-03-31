using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Proto.Cities;

namespace ZeafloServer.Application.gRPC
{ 
    public sealed class CitiesApiImplementation : CitiesApi.CitiesApiBase
    {
        private readonly ICityRepository _cityRepository;

        public CitiesApiImplementation(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public override async Task<GetCitiesByIdsResult> GetByIds(
            GetCitiesByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsGuids = new List<Guid>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsGuids.Add(Guid.Parse(id));
            }

            var cities = await _cityRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(city => idsAsGuids.Contains(city.CityId))
                .Select(city => new GrpcCity
                {
                    Id = city.Id.ToString(),
                    Name = city.Name,
                    PostalCode = city.PostalCode,
                    Latitude = city.Latitude,
                    Longitude = city.Longitude,
                    IsDeleted = city.Deleted
                }).ToListAsync();

            var result = new GetCitiesByIdsResult();

            result.Cities.AddRange(cities);

            return result;
        }
    }
}
