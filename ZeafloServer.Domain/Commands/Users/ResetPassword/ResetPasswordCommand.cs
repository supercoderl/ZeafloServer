using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Users.ResetPassword
{
    public sealed class ResetPasswordCommand : CommandBase<bool>, IRequest<bool>
    {
        private static readonly ResetPasswordCommandValidation s_validation = new();

        public string NewPassword { get; }
        public string Token { get; }

        public ResetPasswordCommand(
            string newPassword,
            string token
        ) : base(Guid.NewGuid())
        {
            NewPassword = newPassword;
            Token = token;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
