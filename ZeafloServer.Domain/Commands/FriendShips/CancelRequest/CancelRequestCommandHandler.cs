using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Errors;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;

namespace ZeafloServer.Domain.Commands.FriendShips.CancelRequest
{
    public sealed class CancelRequestCommandHandler : CommandHandlerBase<bool>, IRequestHandler<CancelRequestCommand, bool>
    {
        private readonly IFriendShipRepository _friendShipRepository;

        public CancelRequestCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IFriendShipRepository friendShipRepository
        ) : base(bus, unitOfWork, notifications )
        {
            _friendShipRepository = friendShipRepository;
        }

        public async Task<bool> Handle(CancelRequestCommand request, CancellationToken cancellationToken)
        {
            if(!await TestValidityAsync(request)) return false;

            FriendShip? friendShip = null;

            if(request.FriendShipId.HasValue)
            {
                friendShip = await _friendShipRepository.GetByIdAsync(request.FriendShipId.Value);
            }
            else if(request.UserId.HasValue && request.FriendId.HasValue)
            {
                friendShip = await _friendShipRepository.GetByUserAndFriendAsync(request.UserId.Value, request.FriendId.Value);
            }

            if (friendShip == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"You have not sent or received any invitation request.",
                    ErrorCodes.ObjectNotFound
                ));
                return false;
            }

            _friendShipRepository.Remove(friendShip, true);

            return await CommitAsync();
        }
    }
}
