using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;

namespace ZeafloServer.Domain.Commands.Users.ResetPassword
{
    public sealed class ResetPasswordCommandHandler : CommandHandlerBase<bool>, IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
        private readonly IUserRepository _userRepository;

        public ResetPasswordCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IPasswordResetTokenRepository passwordResetTokenRepository,
            IUserRepository userRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _passwordResetTokenRepository = passwordResetTokenRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return false;

            var code = await _passwordResetTokenRepository.GetByCodeAsync(request.Token);

            if(code == null) return false;

            return await UpdatePassword(request.NewPassword, code.UserId);
        }

        private async Task<bool> UpdatePassword(string newPassword, Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null) return false;

            user.SetPasswordHash(BCrypt.Net.BCrypt.HashPassword(newPassword));

            _userRepository.Update(user);

            return await CommitAsync();
        }
    }
}
