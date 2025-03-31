using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Shared.Events.PostMedia;

namespace ZeafloServer.Domain.Commands.PostMedias.CreatePostMedia
{
    public sealed class CreatePostMediaCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<CreatePostMediaCommand, Guid>
    {
        private readonly IPostMediaRepository _postMediaRepository;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public CreatePostMediaCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IPostMediaRepository postMediaRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _postMediaRepository = postMediaRepository;
        }

        public async Task<Guid> Handle(CreatePostMediaCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return Guid.Empty;

            var postMedia = new Entities.PostMedia(
                request.PostMediaId,
                request.PostId,
                request.MediaUrl,
                request.MediaType,
                TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone)
            );

            _postMediaRepository.Add( postMedia );

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new PostMediaCreatedEvent(postMedia.PostMediaId));
            }

            return postMedia.PostMediaId;
        }
    }
}
