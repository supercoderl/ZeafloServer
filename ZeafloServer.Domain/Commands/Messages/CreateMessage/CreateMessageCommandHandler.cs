using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Shared.Events.Message;

namespace ZeafloServer.Domain.Commands.Messages.CreateMessage
{
    public sealed class CreateMessageCommandHandler : CommandHandlerBase<Message?>, IRequestHandler<CreateMessageCommand, Message?>
    {
        private readonly IMessageRepository _messageRepository;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public CreateMessageCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IMessageRepository messageRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Message?> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            if(!await TestValidityAsync(request)) return null;

            var message = new Entities.Message(
                request.MessageId,
                request.SenderId,
                request.ReceiverId,
                request.Content,
                request.MediaUrl,
                false,
                TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone)
            );

            _messageRepository.Add(message);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new MessageCreatedEvent(message.MessageId));
            }

            return message;
        }
    }
}
