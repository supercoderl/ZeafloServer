using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Plans.CreatePlan
{
    public sealed class CreatePlanCommand : CommandBase<Guid>, IRequest<Guid>
    {
        private static readonly CreatePlanCommandValidation s_validation = new();

        public Guid PlanId { get; }
        public string Name { get; }
        public string? Description { get; }
        public double MonthlyPrice { get; }
        public double AnnualPrice { get; }
        public int MaxSeats { get; }

        public CreatePlanCommand(
            Guid planId,
            string name,
            string? description,
            double monthlyPrice,
            double annualPrice,
            int maxSeats
        ) : base(Guid.NewGuid())
        {
            PlanId = planId;
            Name = name;
            Description = description;
            MonthlyPrice = monthlyPrice;
            AnnualPrice = annualPrice;
            MaxSeats = maxSeats;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
