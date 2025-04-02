using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Places.CreatePlace
{
    public sealed class CreatePlaceCommandValidation : AbstractValidator<CreatePlaceCommand>
    {
        public CreatePlaceCommandValidation()
        {
            RuleForName();
        }

        private void RuleForName()
        {
            RuleFor(cmd => cmd.Name).NotEmpty().WithErrorCode(DomainErrorCodes.Place.EmptyName).WithMessage("Name may not be empty.");
        }
    }
}
