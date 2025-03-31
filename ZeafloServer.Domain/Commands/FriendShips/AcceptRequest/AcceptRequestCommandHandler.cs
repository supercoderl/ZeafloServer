using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;

namespace ZeafloServer.Domain.Commands.FriendShips.AcceptRequest
{
    public sealed class AcceptRequestCommandHandler : CommandHandlerBase<bool>, IRequestHandler<AcceptRequestCommand, bool>
    {
        private readonly IFriendShipRepository _friendShipRepository;

        public AcceptRequestCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IFriendShipRepository friendShipRepository
        ) : base(bus, unitOfWork, notifications )
        {
            _friendShipRepository = friendShipRepository;
        }

        public async Task<bool> Handle(AcceptRequestCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return false;

            var friendShip = await _friendShipRepository.GetByIdAsync(request.FriendShipId);

            if(friendShip == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"You have not any invitation request!",
                    ErrorCodes.ObjectNotFound
                ));
                return false;
            }

            friendShip.SetFriendShipStatus(Enums.FriendShipStatus.Accepted);

            return await CommitAsync();
        }
    }
}
