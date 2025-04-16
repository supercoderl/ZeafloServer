using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.Plans.CreatePlan
{
    public sealed class CreatePlanCommandValidation : AbstractValidator<CreatePlanCommand>
    {
        public CreatePlanCommandValidation()
        {
            RuleForName();
        }

        private void RuleForName()
        {
            RuleFor(cmd => cmd.Name).NotEmpty().WithErrorCode(DomainErrorCodes.Plan.EmptyName).WithMessage("Name may not be empty.");
        }
    }
}
