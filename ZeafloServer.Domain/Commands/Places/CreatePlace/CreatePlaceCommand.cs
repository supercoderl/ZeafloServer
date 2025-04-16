using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Commands.Places.CreatePlace
{
    public sealed class CreatePlaceCommand : CommandBase<Guid>, IRequest<Guid>
    {
        private static readonly CreatePlaceCommandValidation s_validation = new();

        public Guid PlaceId { get; }
        public string Name { get; }
        public string Address { get; }
        public string? Description { get; }
        public PlaceType Type { get; }
        public Guid CityId { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public double Rating { get; }
        public int ReviewCount { get; }
        public bool IsOpen { get; }

        public CreatePlaceCommand(
            Guid placeId,
            string name,
            string address,
            string? description,    
            PlaceType type,
            Guid cityId,
            double latitude,
            double longitude,
            double rating,
            int reviewCount,
            bool isOpen
        ) : base(Guid.NewGuid())
        {
            PlaceId = placeId;
            Name = name;
            Address = address;
            Description = description;
            Type = type;
            CityId = cityId;
            Latitude = latitude;
            Longitude = longitude;
            Rating = rating;
            ReviewCount = reviewCount;
            IsOpen = isOpen;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
