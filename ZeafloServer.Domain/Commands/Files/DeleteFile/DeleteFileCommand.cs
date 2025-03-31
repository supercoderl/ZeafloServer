using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Files.DeleteFile
{
    public sealed class DeleteFileCommand : CommandBase<bool>, IRequest<bool>
    {
        private static readonly DeleteFileCommandValidation s_validation = new();

        public string PublicId { get; }

        public DeleteFileCommand(string publicId) : base(Guid.NewGuid())
        {
            PublicId = publicId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
