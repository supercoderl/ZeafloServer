using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.TripDurations.CreateTripDuration
{
    public sealed class CreateTripDurationCommandValidation : AbstractValidator<CreateTripDurationCommand>
    {
        public CreateTripDurationCommandValidation()
        {
            RuleForLabel();
        }

        private void RuleForLabel()
        {
            RuleFor(cmd => cmd.Label).NotEmpty().WithErrorCode(DomainErrorCodes.TripDuration.EmptyLabel).WithMessage("Label may not be empty.");
        }
    }
}
