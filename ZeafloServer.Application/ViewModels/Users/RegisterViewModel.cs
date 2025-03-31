using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.ViewModels.Users
{
    public sealed record RegisterViewModel(
        string Username,
        string Email,
        string Password,
        string Fullname,
        string? Bio,
        string AvatarUrl,
        string CoverPhotoUrl,
        string PhoneNumber,
        string? Website,
        string? Location,
        DateTime Birthdate,
        Gender Gender
    );
}
