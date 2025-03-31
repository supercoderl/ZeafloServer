using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Commands.Users.UpdateUser
{
    public sealed class UpdateUserCommand : CommandBase<User?>, IRequest<User?>
    {
        private static readonly UpdateUserCommandValidation s_validation = new();

        public Guid UserId { get; }
        public string Username { get; }
        public string Email { get; }
        public string Fullname { get; }
        public string? Bio {  get; }
        public string AvatarUrl { get; }
        public string CoverPhotoUrl { get; }
        public string PhoneNumber { get; }
        public string? Website { get; }
        public string? Location { get; }
        public string QrUrl { get; }
        public Gender Gender { get; }
        public bool IsOnline { get; }
        public DateTime? LastLoginTime { get; }

        public UpdateUserCommand(
            Guid userId,
            string username,
            string email,
            string fullname,
            string? bio,
            string avatarUrl,
            string coverPhotoUrl,
            string phoneNumber,
            string? website,
            string? location,
            string qrUrl,
            Gender gender,
            bool isOnline,
            DateTime? lastLoginTime
        ) : base(Guid.NewGuid())
        {
            UserId = userId;
            Username = username;
            Email = email;
            Fullname = fullname;
            Bio = bio;
            AvatarUrl = avatarUrl;
            CoverPhotoUrl = coverPhotoUrl;
            PhoneNumber = phoneNumber;
            Website = website;
            Location = location;
            QrUrl = qrUrl;
            Gender = gender;
            IsOnline = isOnline;
            LastLoginTime = lastLoginTime;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
