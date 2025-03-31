using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Users.Login
{
    public sealed class LoginCommand : CommandBase<object?>, IRequest<object?>
    {
        private static readonly LoginCommandValidation s_validation = new();

        public string Identifier { get; }
        public string Password { get; }

        public LoginCommand(string identifier, string password) : base(Guid.NewGuid())
        {
            Identifier = identifier;
            Password = password;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
