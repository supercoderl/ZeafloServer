using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Commands.Points.AddPoint
{
    public sealed class AddPointCommand : CommandBase<Guid>, IRequest<Guid>
    {
        private static readonly AddPointCommandValidation s_validation = new();

        public Guid UserId { get; }
        public ActionType ActionType { get; }

        public AddPointCommand(
            Guid userId,
            ActionType actionType
        ) : base(Guid.NewGuid())
        {
            UserId = userId;
            ActionType = actionType;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
