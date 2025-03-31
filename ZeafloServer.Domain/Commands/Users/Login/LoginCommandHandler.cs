using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;
using ZeafloServer.Domain.Helpers;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Domain.Settings;

namespace ZeafloServer.Domain.Commands.Users.Login
{
    public sealed class LoginCommandHandler : CommandHandlerBase<object?>, IRequestHandler<LoginCommand, object?>
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenHelpers _tokenHelpers;
        private const double _expiryDurationMinutes = 60;
        private readonly TokenSettings _tokenSettings;

        public LoginCommandHandler(
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            TokenHelpers tokenHelpers,
            IOptions<TokenSettings> tokenSettings
        ) : base(bus, unitOfWork, notifications)
        {
            _userRepository = userRepository;
            _tokenHelpers = tokenHelpers;
            _tokenSettings = tokenSettings.Value;
        }

        public async Task<object?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return null;

            var user = await _userRepository.GetByIdentifierAsync(request.Identifier);

            if (user == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There are no user with identifier {request.Identifier}.",
                    ErrorCodes.ObjectNotFound
                ));
                return null;
            }

            bool isCorrect = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isCorrect)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType, 
                    $"Password is incorrect.", 
                    DomainErrorCodes.User.PasswordIncorrect
                ));
                return null;
            }

            var accessTokenResult = _tokenHelpers.BuildToken(user, _tokenSettings, _expiryDurationMinutes);

            string? refreshToken = await _tokenHelpers.BuildRefreshToken(user, accessTokenResult.AccessToken, Bus);

            if (refreshToken == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType, 
                    $"An error occured when creating token.", 
                    DomainErrorCodes.User.TokenBuiltError
                ));
                return null;
            }

            return new
            {
                AccessToken = accessTokenResult.AccessToken,
                ExpiredTime = accessTokenResult.ExpiredTime,
                RefreshToken = refreshToken,
                UserId = user.UserId
            };
        }
    }
}
