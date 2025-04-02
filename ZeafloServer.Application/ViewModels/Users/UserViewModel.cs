using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.ViewModels.Users
{
    public sealed class UserViewModel
    {
        public Guid UserId {  get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Fullname {  get; set; } = string.Empty;
        public string? Bio {  get; set; }
        public string AvatarUrl { get; set; } = Domain.Constants.Profile.User.Avatar;
        public string CoverPhotoUrl { get; set; } = Domain.Constants.Profile.User.Background;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Website {  get; set; }
        public string? Location { get; set; }
        public string QrUrl { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int FriendsCount { get; set; }
        public UserLevelInfo? UserLevel { get; set; }

        public static UserViewModel FromUser(
            User user, 
            int friendCount,
            UserLevelInfo? userLevel
        )
        {
            return new UserViewModel
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Fullname = user.Fullname,
                Bio = user.Bio,
                AvatarUrl = user.AvatarUrl,
                CoverPhotoUrl = user.CoverPhotoUrl,
                PhoneNumber = user.PhoneNumber,
                Website = user.Website,
                Location = user.Location,
                QrUrl = user.QrUrl,
                Birthdate = user.BirthDate,
                Gender = user.Gender,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                FriendsCount = friendCount,
                UserLevel = userLevel
            };
        }
    }

    public class UserLevelInfo
    {
        public LevelType LevelType { get; set; }
        public int ZeafloPoint { get; set; }

        public UserLevelInfo(UserLevel? userLevel)
        {
            LevelType = userLevel?.MemberShipLevel?.Type ?? LevelType.Silver;
            ZeafloPoint = userLevel?.ZeafloPoint ?? 0;
        }
    }
}
