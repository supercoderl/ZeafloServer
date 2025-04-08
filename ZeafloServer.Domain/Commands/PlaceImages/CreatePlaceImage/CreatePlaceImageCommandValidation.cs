using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.PlaceImages.CreatePlaceImage
{
    public sealed class CreatePlaceImageCommandValidation : AbstractValidator<CreatePlaceImageCommand>
    {
        public CreatePlaceImageCommandValidation()
        {
            RuleForImageUrl();
        }

        private void RuleForImageUrl()
        {
            RuleFor(cmd => cmd.ImageUrl).NotEmpty().WithErrorCode(DomainErrorCodes.PlaceImage.EmptyImageUrl).WithMessage("Image url may not be empty.");
        }
    }
}
