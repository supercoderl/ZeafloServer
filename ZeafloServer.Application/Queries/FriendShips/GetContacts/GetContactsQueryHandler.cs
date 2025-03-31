using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.FriendShips;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces.IRepositories;

namespace ZeafloServer.Application.Queries.FriendShips.GetContacts
{
    public sealed class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, PageResult<ContactInfo>>
    {
        private readonly IUserRepository _userRepository;

        public GetContactsQueryHandler(
            IUserRepository userRepository
        )
        {
            _userRepository = userRepository;
        }

        public async Task<PageResult<ContactInfo>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            var friends = _userRepository.GetAllNoTracking()
                .Where(u => u.UserId == request.UserId)
                .SelectMany(u => u.FriendShips
                    .Where(fr => fr.Status == Domain.Enums.FriendShipStatus.Accepted)
                    .Select(fr => fr.FriendId))
                .Union(
                    _userRepository.GetAllNoTracking()
                        .Where(u => u.UserId == request.UserId)
                        .SelectMany(u => u.Friends
                            .Where(fr => fr.Status == Domain.Enums.FriendShipStatus.Accepted)
                            .Select(fr => fr.UserId))
                );

            var query = _userRepository.GetAllNoTracking().IgnoreQueryFilters();

            query = request.Status switch
            {
                ActionStatus.Deleted => query.Where(x => x.Deleted),
                ActionStatus.NotDeleted => query.Where(x => !x.Deleted),
                _ => query
            };

            //Remove accepted friends
            query = query.Where(x => x.UserId != request.UserId && !friends.Contains(x.UserId));

            var totalCount = await query.CountAsync(cancellationToken);

            query = query
                .Skip((request.Query.PageIndex - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize);

            var contacts = await query.Select(contact => new ContactInfo
            {
                FriendId = contact.UserId,
                Username = contact.Username,
                Fullname = contact.Fullname,
                AvatarUrl = contact.AvatarUrl,
                Status = contact.FriendShips.Any(fr =>
                    fr.FriendId == request.UserId && fr.Status == Domain.Enums.FriendShipStatus.Pending)
                    || contact.Friends.Any(fr =>
                        fr.UserId == request.UserId && fr.Status == Domain.Enums.FriendShipStatus.Pending)
                    ? FriendShipStatus.Pending
                    : FriendShipStatus.None
            }).ToListAsync();

            return new PageResult<ContactInfo>(
                totalCount,
                contacts,
                request.Query.PageIndex,
                request.Query.PageSize
            );
        }
    }
}
