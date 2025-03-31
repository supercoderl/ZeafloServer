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
using ZeafloServer.Shared.Events.Token;

namespace ZeafloServer.Domain.Commands.Tokens.UpdateToken
{
    public sealed class UpdateTokenCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<UpdateTokenCommand, Guid>
    {
        private readonly ITokenRepository _tokenRepository;

        public UpdateTokenCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ITokenRepository tokenRepository
        ) : base(bus, unitOfWork, notifications )
        {
            _tokenRepository = tokenRepository;
        }

        public async Task<Guid> Handle(UpdateTokenCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return Guid.Empty;

            var token = await _tokenRepository.GetByIdAsync(request.TokenId);

            if(token == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no token with id {request.TokenId}.",
                    ErrorCodes.ObjectNotFound
                ));
                return Guid.Empty;
            }

            token.SetAccessToken(request.AccessToken);
            token.SetRefreshToken(request.RefreshToken);
            token.SetUserId(request.UserId);
            token.SetIsRefreshTokenRevoked(request.IsRefreshTokenRevoked);
            token.SetRefreshTokenExpiredDate(request.RefreshTokenExpiredDate);

            _tokenRepository.Update(token);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new TokenUpdatedEvent(request.TokenId));
            }

            return request.TokenId;
        }
    }
}
