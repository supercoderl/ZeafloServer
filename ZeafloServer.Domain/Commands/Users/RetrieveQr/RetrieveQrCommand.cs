using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Users.RetrieveQr
{
    public sealed class RetrieveQrCommand : CommandBase<string>, IRequest<string>
    {
        private static readonly RetrieveQrCommandValidation s_validation = new();

        public Guid UserId { get; }

        public RetrieveQrCommand(
            Guid userId
        ) : base(Guid.NewGuid())
        {
            UserId = userId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
