using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Commands.MemberShipLevels.CreateMemberShipLevel
{
    public sealed class CreateMemberShipLevelCommand : CommandBase<Guid>, IRequest<Guid>
    {
        private static readonly CreateMemberShipLevelCommandValidation s_validation = new();

        public Guid MemberShipLevelId { get; }
        public LevelType Type { get; }
        public int MinPoint { get; }

        public CreateMemberShipLevelCommand(
            Guid memberShipLevelId,
            LevelType type,
            int minPoint
        ) : base(memberShipLevelId)
        {
            MemberShipLevelId = memberShipLevelId;
            Type = type;
            MinPoint = minPoint;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
