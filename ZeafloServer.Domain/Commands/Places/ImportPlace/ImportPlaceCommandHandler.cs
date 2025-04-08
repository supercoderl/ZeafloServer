using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Errors;
using ZeafloServer.Domain.Helpers;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;
using static ZeafloServer.Domain.Helpers.FileHelpers;

namespace ZeafloServer.Domain.Commands.Places.ImportPlace
{
    public class PlaceDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public PlaceType Type { get; set; }
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Rating { get; set; }
        public int ReviewCount { get; set; }
        public bool IsOpen { get; set; }
    }

    public sealed class ImportPlaceCommandHandler : CommandHandlerBase<List<Guid>>, IRequestHandler<ImportPlaceCommand, List<Guid>>
    {
        private readonly IPlaceRepository _placeRepository;
        private readonly ICityRepository _cityRepository;
        private readonly FileHelpers _fileHelpers;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public ImportPlaceCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IPlaceRepository placeRepository,
            ICityRepository cityRepository,
            FileHelpers fileHelpers
        ) : base(bus, unitOfWork, notifications)
        {
            _placeRepository = placeRepository;
            _cityRepository = cityRepository;
            _fileHelpers = fileHelpers;
        }
        public async Task<List<Guid>> Handle(ImportPlaceCommand request, CancellationToken cancellationToken)
        {
            var rows = _fileHelpers.ParseExcel<PlaceDTO>(request.Stream, request.Headers);
            
            if(rows.Errors.Count > 0)
            {
                await NotifyAsync(
                    "ImportPlace", 
                    $"There are some errors in the file. Details: \n{WriteError(rows.Errors)}", 
                    ErrorCodes.InvalidFile
                );
                return new List<Guid>();
            }

            var places = new List<Entities.Place>();

            foreach (var row in rows.ValidItems)
            {
                var cityName = await ConvertCityName(row.City);

                var place = new Entities.Place(
                    Guid.NewGuid(),
                    row.Name,
                    row.Address,
                    row.Type,
                    cityName,
                    row.Latitude,
                    row.Longitude,
                    row.Rating,
                    row.ReviewCount,
                    row.IsOpen,
                    TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone),
                    null
                );

                places.Add(place);
            }

            _placeRepository.AddRange(places);

            await CommitAsync();

            return places.Select(p => p.PlaceId).ToList();
        }

        private async Task<Guid> ConvertCityName(string cityName)
        {
            var city = await _cityRepository.GetCityByNameAsync(cityName);

            if (city != null)
            {
                return city.CityId;
            }

            return Constants.Ids.Seed.GuidId;
        }

        private string WriteError(List<RowError> rowErrors)
        {
            var sb = new StringBuilder();
            foreach (var error in rowErrors)
            {
                sb.AppendLine($"Row {error.RowNumber}: {error.ErrorMessage}");
            }
            return sb.ToString();
        }
    }
}
