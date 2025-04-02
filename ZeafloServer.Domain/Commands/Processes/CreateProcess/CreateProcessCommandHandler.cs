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
using ZeafloServer.Shared.Events.Process;

namespace ZeafloServer.Domain.Commands.Processes.CreateProcess
{
    public sealed class CreateProcessCommandHandler : CommandHandlerBase<Processing?>, IRequestHandler<CreateProcessCommand, Processing?>
    {
        private readonly IProcessingRepository _processingRepository;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public CreateProcessCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IProcessingRepository processingRepository
        ) : base(bus, unitOfWork, notifications ) 
        {
            _processingRepository = processingRepository;
        }

        public async Task<Processing?> Handle(CreateProcessCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return null;

            var process = new Entities.Processing(
                request.ProcessingId,
                request.UserId,
                request.Type,
                request.Status,
                TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone),
                null
            );

            _processingRepository.Add( process );

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new ProcessCreatedEvent(process.ProcessingId));
            }

            return process;
        }
    }
}
