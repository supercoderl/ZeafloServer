using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Posts.SavePost
{
    public sealed class SavePostCommandValidation : AbstractValidator<SavePostCommand>  
    {
        public SavePostCommandValidation()
        {
            RuleForUserId();
            RuleForPostId();
        }

        private void RuleForUserId()
        {
            RuleFor(cmd => cmd.UserId).NotEmpty().WithErrorCode(DomainErrorCodes.SavePost.EmptyUserId).WithMessage("User id may not be empty.");
        }

        private void RuleForPostId()
        {
            RuleFor(cmd => cmd.PostId).NotEmpty().WithErrorCode(DomainErrorCodes.SavePost.EmptyPostId).WithMessage("Post id may not be empty.");
        }
    }
}
