using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Places.ReactPlace
{
    public sealed class ReactPlaceCommand : CommandBase<Guid>, IRequest<Guid>
    {
        private static readonly ReactPlaceCommandValidation s_validation = new();

        public Guid PlaceLikeId { get; }
        public Guid PlaceId { get; }
        public Guid UserId { get; }

        public ReactPlaceCommand(
            Guid placeLikeId,
            Guid placeId,
            Guid userId
        ) : base(Guid.NewGuid())
        {
            PlaceLikeId = placeLikeId;
            PlaceId = placeId;
            UserId = userId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
