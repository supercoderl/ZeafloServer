using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Commands.Tokens.UpdateToken;
using ZeafloServer.Domain.Errors;
using ZeafloServer.Domain.Helpers;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Domain.Settings;

namespace ZeafloServer.Domain.Commands.Users.RefreshToken
{
    public sealed class RefreshTokenCommandHandler : CommandHandlerBase<object?>, IRequestHandler<RefreshTokenCommand, object?>
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly TokenHelpers _tokenHelpers;
        private readonly IUserRepository _userRepository;
        private readonly TokenSettings _tokenSettings;
        private const double _expiryDurationMinutes = 60;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public RefreshTokenCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ITokenRepository tokenRepository,
            TokenHelpers tokenHelpers,
            IUserRepository userRepository,
            IOptions<TokenSettings> tokenSettings
        ) : base(bus, unitOfWork, notifications ) 
        {
            _tokenRepository = tokenRepository;
            _tokenHelpers = tokenHelpers;
            _userRepository = userRepository;
            _tokenSettings = tokenSettings.Value;
        }

        public async Task<object?> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return null;

            var token = await _tokenRepository.GetByRefreshTokenAsync(request.RefreshToken);

            if (token == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no token with refresh token {request.RefreshToken}.",
                    ErrorCodes.ObjectNotFound
                ));

                return null;
            }

            if(token.IsRefreshTokenRevoked)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"Token {request.RefreshToken} was revoked.",
                    ErrorCodes.ObjectNotFound
                ));

                return null;
            }

            if(token.RefreshTokenExpiredDate < TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone))
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"Token {request.RefreshToken} was expired.",
                    ErrorCodes.TokenExpired
                ));

                return null;
            }

            var user = await _userRepository.GetByIdAsync(token.UserId);

            if (user == null) {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no user with id {token.UserId}.",
                    ErrorCodes.ObjectNotFound
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

            await Bus.SendCommandAsync(new UpdateTokenCommand(
                token.TokenId,
                token.AccessToken,
                token.RefreshToken,
                token.UserId,
                true,
                token.RefreshTokenExpiredDate
            ));

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
