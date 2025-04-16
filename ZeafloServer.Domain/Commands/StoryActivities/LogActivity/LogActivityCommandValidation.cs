using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.StoryActivities.LogActivity
{
    public sealed class LogActivityCommandValidation : AbstractValidator<LogActivityCommand>
    {
        public LogActivityCommandValidation()
        {
            
        }
    }
}
