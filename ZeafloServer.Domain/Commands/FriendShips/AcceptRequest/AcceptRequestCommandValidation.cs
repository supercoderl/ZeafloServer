using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.FriendShips.AcceptRequest
{
    public sealed class AcceptRequestCommandValidation : AbstractValidator<AcceptRequestCommand>
    {
        public AcceptRequestCommandValidation()
        {
            RuleForId();
        }

        private void RuleForId()
        {
            RuleFor(cmd => cmd.FriendShipId).NotEmpty().WithErrorCode(DomainErrorCodes.FriendShip.EmptyFriendShipId).WithMessage("Id may not be empty.");
        }
    }
}
