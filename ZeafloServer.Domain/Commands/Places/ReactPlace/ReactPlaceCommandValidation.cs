using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Places.ReactPlace
{
    public sealed class ReactPlaceCommandValidation : AbstractValidator<ReactPlaceCommand>
    {
        public ReactPlaceCommandValidation()
        {
            
        }
    }
}
