using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Extensions;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels.Users;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces.IRepositories;

namespace ZeafloServer.Application.Queries.Users.GetAll
{
    public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PageResult<UserViewModel>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISortingExpressionProvider<UserViewModel, User> _sortingExpressionProvider;

        public GetAllUsersQueryHandler(
            IUserRepository userRepository,
            ISortingExpressionProvider<UserViewModel, User> sortingExpressionProvider
        )
        {
            _userRepository = userRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
        }

        public async Task<PageResult<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var query = _userRepository.GetAllNoTracking().IgnoreQueryFilters();

            query = request.Status switch
            {
                ActionStatus.Deleted => query.Where(x => x.Deleted),
                ActionStatus.NotDeleted => query.Where(x => !x.Deleted),
                _ => query
            };

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                query = query.Where(x =>
                    (x.Username.Contains(request.SearchTerm)) ||
                    (x.Email.Contains(request.SearchTerm)) ||
                    (x.Fullname.Contains(request.SearchTerm)) ||
                    (x.PhoneNumber.Contains(request.SearchTerm)
               ));

            var totalCount = await query.CountAsync(cancellationToken);

            query = query.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            query = query
                .Skip((request.Query.PageIndex - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize);

            var users = await query.Select(user => UserViewModel.FromUser(user)).ToListAsync();

            return new PageResult<UserViewModel>(
                totalCount,
                users,
                request.Query.PageIndex,
                request.Query.PageSize
            );
        }
    }
}
