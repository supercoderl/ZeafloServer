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
using ZeafloServer.Shared.Events.User;

namespace ZeafloServer.Domain.Commands.Users.UpdateUser
{
    public sealed class UpdateUserCommandHandler : CommandHandlerBase<User?>, IRequestHandler<UpdateUserCommand, User?>
    {
        private readonly IUserRepository _userRepository;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public UpdateUserCommandHandler(
           IMediatorHandler bus,
           IUnitOfWork unitOfWork,
           INotificationHandler<DomainNotification> notifications,
           IUserRepository userRepository
        ) : base (bus, unitOfWork, notifications)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return null;

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if(user == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no user with id {request.UserId}.",
                    ErrorCodes.ObjectNotFound
                ));
                return null;
            }

            user.SetUsername(request.Username);
            user.SetEmail(request.Email);
            user.SetFullName(request.Fullname);
            user.SetBio(request.Bio);
            user.SetAvatarUrl(request.AvatarUrl);
            user.SetCoverPhotoUrl(request.CoverPhotoUrl);
            user.SetPhoneNumber(request.PhoneNumber);
            user.SetWebsite(request.Website);
            user.SetLocation(request.Location);
            user.SetQrUrl(request.QrUrl);
            user.SetGender(request.Gender);
            user.SetIsOnline(request.IsOnline);
            user.SetLastLoginTime(request.LastLoginTime);
            user.SetUpdatedAt(TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone));

            _userRepository.Update(user);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new UserUpdatedEvent(request.UserId));
            }

            return user;
        }
    }
}
