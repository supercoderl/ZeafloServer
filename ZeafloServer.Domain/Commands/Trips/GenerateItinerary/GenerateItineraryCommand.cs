using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Trips.GenerateItinerary
{
    public sealed class GenerateItineraryCommand : CommandBase<bool>, IRequest<bool>
    {
        private static readonly GenerateItineraryCommandValidation s_validation = new();

        public Guid CityId { get; }
        public Guid TripDurationId { get; }

        public GenerateItineraryCommand(
            Guid cityId,
            Guid tripDurationId
        ) : base(Guid.NewGuid())
        {
            CityId = cityId;
            TripDurationId = tripDurationId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
