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

namespace ZeafloServer.Domain.Commands.Posts.ReactPost
{
    public sealed class ReactPostCommandHandler : CommandHandlerBase<Guid>, IRequestHandler<ReactPostCommand, Guid>
    {
        private readonly IPostReactionRepository _postReactionRepository;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public ReactPostCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IPostReactionRepository postReactionRepository
        ) : base(bus, unitOfWork, notifications )   
        {
            _postReactionRepository = postReactionRepository;
        }

        public async Task<Guid> Handle(ReactPostCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return Guid.Empty;
            bool isEdited = false;

            var postReaction = await _postReactionRepository.GetByPostAndUserAsync(request.PostId, request.UserId);

            if (postReaction != null)
            {
                if (postReaction.ReactionType == request.ReactionType)
                {
                    // If duplicated reaction => Remove from database
                    _postReactionRepository.Remove(postReaction, true);
                }
                else
                {
                    postReaction.SetReactionType(request.ReactionType);
                    _postReactionRepository.Update(postReaction);
                    isEdited = true;
                }
            }
            else
            {
                postReaction = new Entities.PostReaction(
                    request.PostReactionId,
                    request.PostId,
                    request.UserId,
                    request.ReactionType,
                    TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone)
                );

                _postReactionRepository.Add(postReaction);
            }

            if (await CommitAsync())
            {
                if (isEdited)
                {
                    await Bus.RaiseEventAsync(new PostUpdatedEvent(postReaction.PostReactionId));
                }
                else
                {
                    await Bus.RaiseEventAsync(new PostReactedEvent(postReaction.PostReactionId));
                }
            }

            return postReaction.PostReactionId;
        }
    }
}
