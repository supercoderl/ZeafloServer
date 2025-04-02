using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Commands.Processes.CreateProcess
{
    public sealed class CreateProcessCommand : CommandBase<Processing?>, IRequest<Processing?>
    {
        private static readonly CreateProcessCommandValidation s_validation = new();

        public Guid ProcessingId { get; }
        public Guid UserId { get; }
        public string Type { get; }
        public ProcessStatus Status { get; }

        public CreateProcessCommand(
            Guid processingId,
            Guid userId,
            string type,
            ProcessStatus status
        ) : base(Guid.NewGuid())
        {
            ProcessingId = processingId;
            UserId = userId;
            Type = type;
            Status = status;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
