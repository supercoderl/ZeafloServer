using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Shared.Events.FriendShip;

namespace ZeafloServer.Domain.Commands.FriendShips.AddFriend
{
    public sealed class AddFriendCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<AddFriendCommand, Guid>
    {
        private readonly IFriendShipRepository _friendShipRepository;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public AddFriendCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IFriendShipRepository friendShipRepository
        ) : base (bus, unitOfWork, notifications)
        {
            _friendShipRepository = friendShipRepository;
        }

        public async Task<Guid> Handle(AddFriendCommand request, CancellationToken cancellationToken)
        {
            if(!await TestValidityAsync(request)) return Guid.Empty;

            var friendShip = new Entities.FriendShip(
                request.FriendShipId,
                request.UserId,
                request.FriendId,
                Enums.FriendShipStatus.Pending,
                TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone)
            );

            _friendShipRepository.Add(friendShip);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new FriendShipCreatedEvent(friendShip.FriendShipId));
            }

            return friendShip.FriendShipId;
        }
    }
}
