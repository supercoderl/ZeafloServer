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
using ZeafloServer.Shared.Events.Process;

namespace ZeafloServer.Domain.Commands.Processes.UpdateProcess
{
    public sealed class UpdateProcessCommandHandler : CommandHandlerBase<Processing?>, IRequestHandler<UpdateProcessCommand, Processing?>
    {
        private readonly IProcessingRepository _processingRepository;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public UpdateProcessCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IProcessingRepository processingRepository
        ) : base(bus, unitOfWork, notifications ) 
        {
            _processingRepository = processingRepository;
        }

        public async Task<Processing?> Handle(UpdateProcessCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return null;

            var process = await _processingRepository.GetByIdAsync(request.ProcessingId);

            if(process == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any process with id {request.ProcessingId}.",
                    ErrorCodes.ObjectNotFound
                ));

                return null;
            }

            process.SetUserId(request.ProcessingId);
            process.SetType(request.MessageType);
            process.SetStatus(request.Status);
            process.SetUpdatedAt(TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone));

            _processingRepository.Update(process);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new ProcessUpdatedEvent(process.ProcessingId));
            }

            return process;
        }
    }
}
