using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Shared.Events.Post;

namespace ZeafloServer.Domain.Commands.Posts.CreatePost
{
    public sealed class CreatePostCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<CreatePostCommand, Guid>
    {
        private readonly IPostRepository _postRepository;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public CreatePostCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IPostRepository postRepository
        ) : base ( bus, unitOfWork, notifications )
        {
            _postRepository = postRepository;
        }

        public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            if(!await TestValidityAsync(request)) return Guid.Empty;

            var post = new Entities.Post(
                request.PostId,
                request.UserId,
                request.Title,
                request.Content,
                request.ThumbnailUrl,
                request.Visibility,
                TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone)
            );

            _postRepository.Add( post );

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new PostCreatedEvent(post.PostId));
            }

            return post.PostId;
        }
    }
}
