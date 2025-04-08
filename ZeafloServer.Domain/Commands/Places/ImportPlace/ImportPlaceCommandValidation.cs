using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Places.ImportPlace
{
    public sealed class ImportPlaceCommandValidation : AbstractValidator<ImportPlaceCommand>
    {
        public ImportPlaceCommandValidation()
        {
            
        }
    }
}
