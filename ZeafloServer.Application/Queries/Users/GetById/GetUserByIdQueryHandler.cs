using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Users;
using ZeafloServer.Domain.Errors;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;

namespace ZeafloServer.Application.Queries.Users.GetById
{
    public sealed record GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserViewModel?>
    {
        private readonly IMediatorHandler _bus;
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(
            IMediatorHandler bus,
            IUserRepository userRepository
        )
        {
            _bus = bus;
            _userRepository = userRepository;
        }

        public async Task<UserViewModel?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.userId, query =>
                query
                    .Include(f => f.Friends)
                    .Include(fs => fs.FriendShips)
                    .Where(u => u.UserLevel != null)
                    .Include(ul => ul.UserLevel)
                        .ThenInclude(ms => ms!.MemberShipLevel)
                    .Include(us => us.UserSubscriptions)
                        .ThenInclude(us => us.Plan)
            );

            if (user == null)
            {
                await _bus.RaiseEventAsync(new DomainNotification(
                    nameof(GetUserByIdQuery),
                    $"User with id {request.userId} could not be found.",
                    ErrorCodes.ObjectNotFound
                ));

                return null;
            }

            var sub = user.UserSubscriptions
                .FirstOrDefault(us => us.IsTrial || us.IsActive);

            return UserViewModel.FromUser( 
                user,
                user.Friends.Count() + user.FriendShips.Count(),
                new UserLevelInfo(user.UserLevel),
                sub != null ? new UserSubscriptionInfo(sub) : null
            );
        }
    }
}
