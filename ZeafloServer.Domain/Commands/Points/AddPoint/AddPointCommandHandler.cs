using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Commands.StoryActivities.LogActivity;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Errors;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;

namespace ZeafloServer.Domain.Commands.Points.AddPoint
{
    public sealed class AddPointCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<AddPointCommand, Guid>
    {
        private readonly IUserLevelRepository _userLevelRepository;
        private readonly IMemberShipLevelRepository _memberShipLevelRepository;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public AddPointCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IUserLevelRepository userLevelRepository,
            IMemberShipLevelRepository memberShipLevelRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _userLevelRepository = userLevelRepository;
            _memberShipLevelRepository = memberShipLevelRepository;
        }

        public async Task<Guid> Handle(AddPointCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return Guid.Empty;

            var levels = await GetLevelListAsync();

            if (levels.Count() <= 0)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType, 
                    "There is no any level.", 
                    ErrorCodes.ObjectNotFound
                ));
                return Guid.Empty;
            }

            var userLevel = await GetUserLevelAsync(request.UserId, levels);

            var gainedPoint = GetPointForAction(request.ActionType);
            var totalPoint = userLevel.ZeafloPoint + gainedPoint;

            var matchedLevel = levels
                .Where(x => x.MinPoint <= totalPoint)
                .OrderByDescending(x => x.MinPoint)
                .FirstOrDefault();

            if(matchedLevel != null && matchedLevel.MemberShipLevelId != userLevel.MemberShipLevelId)
            {
                userLevel.SetMemberShipLevelId(matchedLevel.MemberShipLevelId);
            }

            userLevel.SetZeafloPoint(userLevel.ZeafloPoint + gainedPoint);
            userLevel.SetAssignedAt(TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone));

            _userLevelRepository.Update(userLevel);

            if(await CommitAsync())
            {
                await Bus.SendCommandAsync(new LogActivityCommand(
                    Guid.NewGuid(),
                    request.UserId,
                    request.ActionType,
                    gainedPoint
                ));
            }

            return userLevel.UserLevelId;
        }

        private async Task<IEnumerable<MemberShipLevel>> GetLevelListAsync()
        {
            var query = _memberShipLevelRepository.GetAllNoTracking().IgnoreQueryFilters();

            return await query.ToListAsync();
        }

        private async Task<UserLevel> GetUserLevelAsync(Guid userId, IEnumerable<MemberShipLevel> levels)
        {
            var userLevel = await _userLevelRepository.GetByUserAsync(userId);
            if (userLevel == null)
            {
                userLevel = new Entities.UserLevel(
                    Guid.NewGuid(),
                    userId,
                    levels.First().MemberShipLevelId,
                    0,
                    TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone)
                );

                _userLevelRepository.Add(userLevel);

                if(!await CommitAsync())
                {
                    await NotifyAsync(new DomainNotification(
                        "Get user level",
                        "Failed to create user level",
                        ErrorCodes.ObjectNotFound
                    ));
                    throw new Exception("Failed to commit changes while creating user level.");
                }
            }

            return userLevel;
        }

        private int GetPointForAction(ActionType actionType)
        {
            return actionType switch
            {
                ActionType.Send => 10,      // send 1 post will gain 10 points
                ActionType.View => 2,       // view people's story will gain 2 points
                ActionType.Receive => 5,    // receive people's story will gain 5 points
                _ => 0
            };
        }
    }
}
