using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.FriendShips.CancelRequest
{
    public sealed class CancelRequestCommandValidation : AbstractValidator<CancelRequestCommand>
    {
        public CancelRequestCommandValidation()
        {

        }
    }
}
