using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Commands.StoryActivities.LogActivity
{
    public sealed class LogActivityCommand : CommandBase<Guid>, IRequest<Guid>
    {
        private static readonly LogActivityCommandValidation s_validation = new();

        public Guid StoryActivityId { get; }
        public Guid UserId { get; }
        public ActionType ActionType { get; }
        public int PointEarned { get; }

        public LogActivityCommand(
            Guid storyActivityId,
            Guid userId,
            ActionType actionType,
            int pointEarned
        ) : base(Guid.NewGuid())
        {
            StoryActivityId = storyActivityId;
            UserId = userId;
            ActionType = actionType;
            PointEarned = pointEarned;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
