using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Application.ViewModels.Users
{
    public sealed record ChangePasswordRequest
    (
        Guid UserId,
        string OldPassword,
        string NewPassword
    );
}
