using MediatR;
using System.Text.RegularExpressions;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Shared.Events.User;

namespace ZeafloServer.Domain.Commands.Users.Register
{
    public sealed class RegisterCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<RegisterCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public RegisterCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IUserRepository userRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return Guid.Empty;

            var user = new Entities.User(
                  request.UserId,
                  request.Username,
                  FormatEmail(request.Email),
                  BCrypt.Net.BCrypt.HashPassword(request.Password),
                  request.Fullname,
                  request.Bio,
                  request.AvatarUrl,
                  request.CoverPhotoUrl,
                  request.PhoneNumber,
                  request.Website,
                  request.Location,
                  request.QrUrl,
                  request.Birthdate,
                  request.Gender,
                  false,
                  null,
                  TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone),
                  null
            );

            _userRepository.Add(user);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new UserCreatedEvent(request.UserId));
            }

            return user.UserId;
        }

        private string FormatEmail(string email)
        {
            string[] parts = email.Split("@");

            return String.Concat(Regex.Replace(parts[0], @"[^a-zA-Z0-9]", string.Empty), "@", parts[1]);
        }
    }
}
