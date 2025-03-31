using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.FriendShips.AcceptRequest
{
    public sealed class AcceptRequestCommand : CommandBase<bool>, IRequest<bool>
    {
        private static readonly AcceptRequestCommandValidation s_validation = new();

        public Guid FriendShipId { get; }

        public AcceptRequestCommand(Guid friendShipId) : base(Guid.NewGuid())
        {
            FriendShipId = friendShipId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
