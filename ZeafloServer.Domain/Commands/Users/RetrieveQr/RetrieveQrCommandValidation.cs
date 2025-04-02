using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Users.RetrieveQr
{
    public sealed class RetrieveQrCommandValidation : AbstractValidator<RetrieveQrCommand>
    {
        public RetrieveQrCommandValidation()
        {
            
        }
    }
}
