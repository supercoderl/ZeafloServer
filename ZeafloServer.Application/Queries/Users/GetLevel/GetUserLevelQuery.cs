using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.UserLevels;
using ZeafloServer.Application.ViewModels.Users;

namespace ZeafloServer.Application.Queries.Users.GetLevel
{
    public sealed record GetUserLevelQuery(Guid userId) : IRequest<UserLevelViewModel?>;
}
