using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Shared.Events.Token;

namespace ZeafloServer.Domain.Commands.Tokens.CreateToken
{
    public sealed class CreateTokenCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<CreateTokenCommand, Guid>
    {
        private readonly ITokenRepository _tokenRepository;

        public CreateTokenCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ITokenRepository tokenRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task<Guid> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return Guid.Empty;

            var token = new Entities.Token(
                request.TokenId,
                request.AccessToken,
                request.RefreshToken,
                request.UserId,
                request.IsRefreshTokenRevoked,
                request.RefreshTokenExpiredDate
            );

            _tokenRepository.Add(token);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new TokenCreatedEvent(token.TokenId));
            }

            return token.TokenId;
        }
    }
}
