using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Commands.Posts.CreatePost
{
    public sealed class CreatePostCommand : CommandBase<Guid>, IRequest<Guid>
    {
        private static readonly CreatePostCommandValidation s_validation = new();

        public Guid PostId { get; }
        public Guid UserId { get; }
        public string Title { get; }
        public string Content { get; }
        public string? ThumbnailUrl { get; }
        public Visibility Visibility { get; }

        public CreatePostCommand(
            Guid postId,
            Guid userId,
            string title,
            string content,
            string? thumbnailUrl,
            Visibility visibility
        ) : base(Guid.NewGuid())
        {
            PostId = postId;
            UserId = userId;
            Title = title;
            Content = content;
            ThumbnailUrl = thumbnailUrl;
            Visibility = visibility;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
