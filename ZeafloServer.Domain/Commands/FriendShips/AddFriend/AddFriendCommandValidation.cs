using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.FriendShips.AddFriend
{
    public sealed class AddFriendCommandValidation : AbstractValidator<AddFriendCommand>
    {
        public AddFriendCommandValidation()
        {
            
        }
    }
}
