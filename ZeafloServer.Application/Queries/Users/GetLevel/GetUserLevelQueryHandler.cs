using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Queries.Users.GetById;
using ZeafloServer.Application.ViewModels.Users;
using ZeafloServer.Domain.Errors;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Application.ViewModels.UserLevels;

namespace ZeafloServer.Application.Queries.Users.GetLevel
{
    public sealed record GetUserLevelQueryHandler : IRequestHandler<GetUserLevelQuery, UserLevelViewModel?>
    {
        private readonly IMediatorHandler _bus;
        private readonly IUserLevelRepository _userLevelRepository;

        public GetUserLevelQueryHandler(
            IMediatorHandler bus,
            IUserLevelRepository userLevelRepository
        )
        {
            _bus = bus;
            _userLevelRepository = userLevelRepository;
        }

        public async Task<UserLevelViewModel?> Handle(GetUserLevelQuery request, CancellationToken cancellationToken)
        {
            var userLevel = await _userLevelRepository.GetByUserAsync(request.userId);

            if (userLevel == null)
            {
                await _bus.RaiseEventAsync(new DomainNotification(
                    nameof(GetUserByIdQuery),
                    $"User level with user id {request.userId} could not be found.",
                    ErrorCodes.ObjectNotFound
                ));

                return null;
            }

            return UserLevelViewModel.FromUserLevel(userLevel);
        }
    }
}
