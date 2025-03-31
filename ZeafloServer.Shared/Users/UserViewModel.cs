using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Enums;

namespace ZeafloServer.Shared.Users
{
    public sealed record UserViewModel(
        Guid UserId,
        string Username,
        string Email,
        string Fullname,
        string? Bio,
        string AvatarUrl,
        string CoverPhotoUrl,
        string PhoneNumber,
        string? Website,
        string? Location,
        string QrUrl,
        DateTime Birthdate,
        Gender Gender,
        bool IsOnline,
        DateTime? LastLoginTime
    );
}
