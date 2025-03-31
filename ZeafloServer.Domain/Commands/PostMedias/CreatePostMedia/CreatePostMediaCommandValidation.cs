using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.PostMedias.CreatePostMedia
{
    public sealed class CreatePostMediaCommandValidation : AbstractValidator<CreatePostMediaCommand>
    {
        public CreatePostMediaCommandValidation()
        {
            RuleForMediaUrl();
        }

        private void RuleForMediaUrl()
        {
            RuleFor(cmd => cmd.MediaUrl).NotEmpty().WithErrorCode(DomainErrorCodes.PostMedia.EmptyMediaUrl).WithMessage("Media url may not be empty.");
        }
    }
}
