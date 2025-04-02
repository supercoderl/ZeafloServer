using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Posts.ReactPost
{
    public sealed class ReactPostCommandValidation : AbstractValidator<ReactPostCommand>
    {
        public ReactPostCommandValidation()
        {
            
        }
    }
}
