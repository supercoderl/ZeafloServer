using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.PlaceImages.CreatePlaceImage
{
    public sealed class CreatePlaceImageCommand : CommandBase<Guid>, IRequest<Guid>
    {
        private static readonly CreatePlaceImageCommandValidation s_validation = new();

        public Guid PlaceImageId { get; }
        public Guid PlaceId { get; }
        public string ImageUrl { get; }

        public CreatePlaceImageCommand(
            Guid placeImageId,
            Guid placeId,
            string imageUrl
        ) : base(Guid.NewGuid())
        {
            PlaceImageId = placeImageId;
            PlaceId = placeId;
            ImageUrl = imageUrl;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
