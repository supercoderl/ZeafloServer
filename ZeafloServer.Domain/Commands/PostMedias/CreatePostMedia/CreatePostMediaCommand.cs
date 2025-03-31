using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Commands.PostMedias.CreatePostMedia
{
    public sealed class CreatePostMediaCommand : CommandBase<Guid>, IRequest<Guid>
    {
        private static readonly CreatePostMediaCommandValidation s_validation = new();

        public Guid PostMediaId { get; }
        public Guid PostId { get; }
        public string MediaUrl { get; }
        public MediaType MediaType { get; }

        public CreatePostMediaCommand(
            Guid postMediaId,
            Guid postId,
            string mediaUrl,
            MediaType mediaType
        ) : base(Guid.NewGuid())
        {
            PostMediaId = postMediaId;
            PostId = postId;
            MediaUrl = mediaUrl;
            MediaType = mediaType;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
