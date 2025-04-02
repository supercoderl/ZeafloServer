using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Shared.Events.MemberShipLevel;

namespace ZeafloServer.Domain.Commands.MemberShipLevels.CreateMemberShipLevel
{
    public sealed class CreateMemberShipLevelCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<CreateMemberShipLevelCommand, Guid>
    {
        private readonly IMemberShipLevelRepository _memberShipLevelRepository;

        public CreateMemberShipLevelCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IMemberShipLevelRepository memberShipLevelRepository
        ) : base( bus, unitOfWork, notifications )
        {
            _memberShipLevelRepository = memberShipLevelRepository;
        }

        public async Task<Guid> Handle(CreateMemberShipLevelCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return Guid.Empty;

            var memberShipLevel = new Entities.MemberShipLevel(
                request.MemberShipLevelId,
                request.Type,
                request.MinPoint
            );

            _memberShipLevelRepository.Add( memberShipLevel );  

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new MemberShipLevelCreatedEvent(memberShipLevel.MemberShipLevelId));
            }

            return memberShipLevel.MemberShipLevelId;
        }
    }
}
