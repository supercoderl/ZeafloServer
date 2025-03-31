using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Files.DeleteFile
{
    public sealed class DeleteFileCommandValidation : AbstractValidator<DeleteFileCommand>
    {
        public DeleteFileCommandValidation()
        {
            RuleForId();
        }

        private void RuleForId()
        {
            RuleFor(x => x.PublicId).NotEmpty().WithErrorCode(DomainErrorCodes.File.EmptyPublicId).WithMessage("Id may not be empty.");
        }
    }
}
