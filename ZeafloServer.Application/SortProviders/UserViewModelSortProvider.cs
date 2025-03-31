using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels.Users;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.SortProviders
{
    public sealed class UserViewModelSortProvider : ISortingExpressionProvider<UserViewModel, User>
    {
        private static readonly Dictionary<string, Expression<Func<User, object>>> s_expressions = new()
        {
            { "username", user => user.Username },
            { "email", user => user.Email },
            { "fullname", user => user.Fullname },
            { "phone_number", user => user.PhoneNumber },
            { "birthdate", user => user.BirthDate },
            { "created_at", user => user.CreatedAt },
            { "updated_at", user => user.UpdatedAt ?? DateTime.Now }
        };

        public Dictionary<string, Expression<Func<User, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
