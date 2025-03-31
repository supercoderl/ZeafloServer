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

namespace ZeafloServer.Domain.Commands.Users.ChangePassword
{
    public sealed class ChangePasswordCommandHandler : CommandHandlerBase<bool>, IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IUserRepository userRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return false;

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if(user == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no user with id {request.UserId}.",
                    ErrorCodes.ObjectNotFound
                ));
                return false;
            }

            if(!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.PasswordHash))
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"Old password is incorrect.",
                    DomainErrorCodes.User.PasswordIncorrect
                ));
                return false;
            }

            user.SetPasswordHash(BCrypt.Net.BCrypt.HashPassword(request.NewPassword));

            _userRepository.Update(user);

            return await CommitAsync();
        }
    }
}
