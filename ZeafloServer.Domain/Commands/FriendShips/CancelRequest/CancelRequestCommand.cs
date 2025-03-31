using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.FriendShips.CancelRequest
{
    public sealed class CancelRequestCommand : CommandBase<bool>, IRequest<bool>
    {
        private static readonly CancelRequestCommandValidation s_validation = new();

        public Guid? FriendShipId { get; }
        public Guid? UserId { get; }
        public Guid? FriendId { get; }

        public CancelRequestCommand(Guid? friendShipId, Guid? userId, Guid? friendId) : base(Guid.NewGuid())
        {
            FriendShipId = friendShipId;
            UserId = userId;
            FriendId = friendId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
