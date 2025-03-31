using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Users;

namespace ZeafloServer.Application.Queries.Users.GetById
{
    public sealed record GetUserByIdQuery(Guid userId) : IRequest<UserViewModel?>;

}
