using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Cities.CreateCity
{
    public sealed class CreateCityCommand : CommandBase<Guid>, IRequest<Guid>
    {
        private static readonly CreateCityCommandValidation s_validation = new();

        public Guid CityId { get; }
        public string Name { get; } 
        public string PostalCode { get; }
        public double Latitude { get; }
        public double Longitude { get; }

        public CreateCityCommand(
            Guid cityId,
            string name,
            string postalCode,
            double latitude,
            double longitude
        ) : base(Guid.NewGuid())
        {
            CityId = cityId;
            Name = name;
            PostalCode = postalCode;
            Latitude = latitude;
            Longitude = longitude;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
