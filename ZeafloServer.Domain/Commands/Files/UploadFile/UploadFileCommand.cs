using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Files.UploadFile
{
    public sealed class UploadFileCommand : CommandBase<string>, IRequest<string>
    {
        private static readonly UploadFileCommandValidation s_validation = new();

        public string Base64String { get; }
        public string FileName { get; }
        public string FolderName { get; }

        public UploadFileCommand(
            string base64String,
            string fileName,
            string folderName
        ) : base(Guid.NewGuid())
        {
            Base64String = base64String;
            FileName = fileName;
            FolderName = folderName;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
