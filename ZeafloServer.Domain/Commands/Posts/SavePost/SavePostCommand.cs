using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Posts.SavePost
{
    public sealed class SavePostCommand : CommandBase<Guid>, IRequest<Guid>
    {
        private static readonly SavePostCommandValidation s_validation = new();

        public Guid SavePostId { get; }
        public Guid PostId { get; }
        public Guid UserId { get; }

        public SavePostCommand(
           Guid savePostId,
           Guid postId,
           Guid userId
        ) : base(Guid.NewGuid())
        {
            SavePostId = savePostId;
            PostId = postId;
            UserId = userId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
