using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Files.UploadFile
{
    public sealed class UploadFileCommandValidation : AbstractValidator<UploadFileCommand>  
    {
        public UploadFileCommandValidation()
        {
            RuleForBase64String();
            RuleForFileName();
            RuleForFolderName();
        }

        private void RuleForBase64String()
        {
            RuleFor(x => x.Base64String).NotEmpty().WithErrorCode(DomainErrorCodes.File.EmptyBase64String).WithMessage("Base 64 string may not be empty.");
        }

        private void RuleForFileName()
        {
            RuleFor(x => x.FileName).NotEmpty().WithErrorCode(DomainErrorCodes.File.EmptyFileName).WithMessage("File name may not be empty.");
        }

        private void RuleForFolderName()
        {
            RuleFor(x => x.FolderName).NotEmpty().WithErrorCode(DomainErrorCodes.File.EmptyFolderName).WithMessage("Folder name may not be empty.");
        }
    }
}
