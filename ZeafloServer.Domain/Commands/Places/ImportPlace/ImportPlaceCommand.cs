using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Places.ImportPlace
{
    public sealed class ImportPlaceCommand : CommandBase<List<Guid>>, IRequest<List<Guid>>
    {
        private static readonly ImportPlaceCommandValidation s_validation = new();

        public Stream Stream { get; }
        public Dictionary<string, string> Headers { get; }

        public ImportPlaceCommand(
            Stream stream,
            Dictionary<string, string> headers
        ) : base(Guid.NewGuid())
        {
            Stream = stream;
            Headers = headers;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
