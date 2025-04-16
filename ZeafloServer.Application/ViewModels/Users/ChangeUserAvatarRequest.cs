using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Application.ViewModels.Users
{
    public sealed record ChangeUserAvatarRequest
    (
        Guid UserId,
        string AvatarBase64String
    );
}
