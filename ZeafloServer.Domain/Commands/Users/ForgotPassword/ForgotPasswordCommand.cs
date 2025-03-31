using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Users.ForgotPassword
{
    public sealed class ForgotPasswordCommand : CommandBase<string>, IRequest
    {
        private static readonly ForgotPasswordCommandValidation s_validation = new();

        public string PhoneNumber { get; }

        public ForgotPasswordCommand(
            string phoneNumber
        ) : base(Guid.NewGuid())
        {
            PhoneNumber = phoneNumber;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
