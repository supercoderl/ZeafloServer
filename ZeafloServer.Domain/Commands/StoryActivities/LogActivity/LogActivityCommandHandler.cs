using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Shared.Events.StoryActivity;

namespace ZeafloServer.Domain.Commands.StoryActivities.LogActivity
{
    public sealed class LogActivityCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<LogActivityCommand, Guid>
    {
        private readonly IStoryActivityRepository _storyActivityRepository;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public LogActivityCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IStoryActivityRepository storyActivityRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _storyActivityRepository = storyActivityRepository;
        }

        public async Task<Guid> Handle(LogActivityCommand request, CancellationToken cancellationToken)
        {
            if(!await TestValidityAsync(request)) return Guid.Empty;

            var storyActivity = new Entities.StoryActivity(
                request.StoryActivityId,
                request.UserId,
                request.ActionType,
                request.PointEarned,
                TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone)
            );

            _storyActivityRepository.Add(storyActivity);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new StoryActivityCreatedEvent(storyActivity.StoryActivityId));
            }

            return storyActivity.StoryActivityId;
        }
    }
}
