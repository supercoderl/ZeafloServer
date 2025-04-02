using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Cities.CreateCity
{
    public sealed class CreateCityCommandValidation : AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidation()
        {
            RuleForName();
            RuleForPostalCode();
        }

        private void RuleForName()
        {
            RuleFor(cmd => cmd.Name).NotEmpty().WithErrorCode(DomainErrorCodes.City.EmptyName).WithMessage("Name may not be empty.");
        }

        private void RuleForPostalCode()
        {
            RuleFor(cmd => cmd.PostalCode).NotEmpty().WithErrorCode(DomainErrorCodes.City.EmptyPostalCode).WithMessage("Postal code may not be empty.");
        }
    }
}
