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

namespace ZeafloServer.Domain.Commands.Posts.SavePost
{
    public sealed class SavePostCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<SavePostCommand, Guid>
    {
        private readonly ISavePostRepository _savePostRepository;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public SavePostCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ISavePostRepository savePostRepository
        ) : base( bus, unitOfWork, notifications )
        {
            _savePostRepository = savePostRepository;
        }

        public async Task<Guid> Handle(SavePostCommand request, CancellationToken cancellationToken)
        {
            if(!await TestValidityAsync(request)) return Guid.Empty;

            var savePost = new Entities.SavePost(
                request.SavePostId,
                request.UserId,
                request.PostId,
                TimeZoneInfo.ConvertTimeToUtc(DateTime.UtcNow, vietnamTimeZone)
            );

            _savePostRepository.Add(savePost);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new PostSavedEvent(savePost.SavePostId));
            }

            return savePost.SavePostId;
        }
    }
}
