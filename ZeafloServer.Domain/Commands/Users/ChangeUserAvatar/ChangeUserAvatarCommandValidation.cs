using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Users.ChangeUserAvatar
{
    public sealed class ChangeUserAvatarCommandValidation : AbstractValidator<ChangeUserAvatarCommand>
    {
        public ChangeUserAvatarCommandValidation()
        {
            RuleForAvatarBase64String();
        }

        private void RuleForAvatarBase64String()
        {
            RuleFor(cmd => cmd.AvatarBase64String).NotEmpty().WithErrorCode(DomainErrorCodes.User.EmptyAvatarBase64String).WithMessage("Avatar may not be empty.");
        }
    }
}
