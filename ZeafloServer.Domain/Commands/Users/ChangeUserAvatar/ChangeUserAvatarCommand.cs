using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Users.ChangeUserAvatar
{
    public sealed class ChangeUserAvatarCommand : CommandBase<string>, IRequest<string>
    {
        private static readonly ChangeUserAvatarCommandValidation s_validation = new();

        public Guid UserId { get; }
        public string AvatarBase64String { get; }

        public ChangeUserAvatarCommand(
            Guid userId,
            string avatarBase64String
        ) : base(Guid.NewGuid())
        {
            UserId = userId;
            AvatarBase64String = avatarBase64String;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
