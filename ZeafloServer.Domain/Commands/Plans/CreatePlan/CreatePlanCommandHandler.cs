using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Shared.Events.Plan;

namespace ZeafloServer.Domain.Commands.Plans.CreatePlan
{
    public sealed class CreatePlanCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<CreatePlanCommand, Guid>
    {
        private readonly IPlanRepository _planRepository;

        public CreatePlanCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IPlanRepository planRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _planRepository = planRepository;
        }

        public async Task<Guid> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            if(!await TestValidityAsync(request)) return Guid.Empty;

            var plan = new Entities.Plan(
                request.PlanId,
                request.Name,
                request.Description,
                request.MonthlyPrice,
                request.AnnualPrice,
                request.MaxSeats
            );

            _planRepository.Add( plan );

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new PlanCreatedEvent(plan.PlanId));
            }

            return plan.PlanId;
        }
    }
}
