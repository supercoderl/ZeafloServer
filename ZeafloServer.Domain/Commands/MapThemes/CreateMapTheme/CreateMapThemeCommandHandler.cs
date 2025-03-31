using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Shared.Events.MapTheme;

namespace ZeafloServer.Domain.Commands.MapThemes.CreateMapTheme
{
    public sealed class CreateMapThemeCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<CreateMapThemeCommand, Guid>
    {
        private readonly IMapThemeRepository _mapThemeRepository;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public CreateMapThemeCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IMapThemeRepository mapThemeRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _mapThemeRepository = mapThemeRepository;
        }

        public async Task<Guid> Handle(CreateMapThemeCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return Guid.Empty;

            var mapTheme = new Entities.MapTheme(
                request.MapThemeId,
                request.Name,
                request.Description,
                request.MapStyle,
                request.PreviewUrl,
                request.IsPremium,
                TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone)
            );

            _mapThemeRepository.Add(mapTheme);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new MapThemeCreatedEvent(mapTheme.MapThemeId));
            }

            return mapTheme.MapThemeId;
        }
    }
}
