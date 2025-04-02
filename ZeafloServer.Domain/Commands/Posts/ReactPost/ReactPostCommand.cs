using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Commands.Posts.ReactPost
{
    public sealed class ReactPostCommand : CommandBase<Guid>, IRequest<Guid>
    {
        private static readonly ReactPostCommandValidation s_validation = new();

        public Guid PostReactionId { get; }
        public Guid PostId { get; }
        public Guid UserId { get; }
        public ReactionType ReactionType { get; }

        public ReactPostCommand(
            Guid postReactionId,
            Guid postId,
            Guid userId,
            ReactionType reactionType
        ) : base (Guid.NewGuid())
        {
            PostReactionId = postReactionId;
            PostId = postId;
            UserId = userId;
            ReactionType = reactionType;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
