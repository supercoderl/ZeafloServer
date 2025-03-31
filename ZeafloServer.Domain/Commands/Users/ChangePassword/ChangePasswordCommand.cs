using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Users.ChangePassword
{
    public sealed class ChangePasswordCommand : CommandBase<bool>, IRequest<bool>
    {
        private static readonly ChangePasswordCommandValidation s_validation = new();

        public Guid UserId { get; }
        public string OldPassword { get; }
        public string NewPassword { get; }

        public ChangePasswordCommand(
            Guid userId,
            string oldPassword,
            string newPassword
        ) : base(Guid.NewGuid())
        {
            UserId = userId;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
