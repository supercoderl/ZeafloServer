using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Commands.FriendShips.AddFriend
{
    public sealed class AddFriendCommand : CommandBase<Guid>, IRequest<Guid>
    {
        private static readonly AddFriendCommandValidation s_validation = new();

        public Guid FriendShipId { get; }
        public Guid UserId { get; }
        public Guid FriendId { get; }

        public AddFriendCommand(
            Guid friendShipId,
            Guid userId,
            Guid friendId
        ) : base(Guid.NewGuid())
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
