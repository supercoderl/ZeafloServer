using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Posts.CreatePost
{
    public sealed class CreatePostCommandValidation : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidation()
        {
            RuleForTitle();
            RuleForContent();
        }

        private void RuleForTitle()
        {
            RuleFor(x => x.Title).NotEmpty().WithErrorCode(DomainErrorCodes.Post.EmptyTitle).WithMessage("Title may not be empty.");
        }

        private void RuleForContent()
        {
            RuleFor(x => x.Content).NotEmpty().WithErrorCode(DomainErrorCodes.Post.EmptyContent).WithMessage("Content may not be empty.");
        }
    }
}
