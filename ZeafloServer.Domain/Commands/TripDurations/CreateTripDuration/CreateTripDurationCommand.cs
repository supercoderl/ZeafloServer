using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.TripDurations.CreateTripDuration
{
    public sealed class CreateTripDurationCommand : CommandBase<Guid>, IRequest<Guid>
    {
        private static readonly CreateTripDurationCommandValidation s_validation = new();

        public Guid TripDurationId { get; }
        public string Label { get; }
        public int Days { get; }
        public int Nights { get; }

        public CreateTripDurationCommand(
            Guid tripDurationId,
            string label,
            int days,
            int nights
        ) : base(tripDurationId)
        {
            TripDurationId = tripDurationId;
            Label = label;
            Days = days;
            Nights = nights;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
