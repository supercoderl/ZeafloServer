using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;

namespace ZeafloServer.Domain.Commands.Messages.UpdateUnreadMessage
{
    public sealed class UpdateUnreadMessageCommandHandler : CommandHandlerBase<bool>, IRequestHandler<UpdateUnreadMessageCommand, bool>
    {
        private readonly IMessageRepository _messageRepository;

        public UpdateUnreadMessageCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IMessageRepository messageRepository
        ) : base( bus, unitOfWork, notifications )
        {
            _messageRepository = messageRepository;
        }

        public async Task<bool> Handle(UpdateUnreadMessageCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return false;

            var messages = await _messageRepository.GetBySenderAndReceiverAsync(request.SenderId, request.ReceiverId);

            if (messages.Count() > 0)
            {
                foreach (var message in messages)
                {
                    message.SetIsRead(true);
                }

                _messageRepository.UpdateRange(messages);
            }

            return await CommitAsync();
        }
    }
}
