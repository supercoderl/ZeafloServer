using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Commands.Users.Register
{
    public sealed class RegisterCommand : CommandBase<Guid>, IRequest<Guid>
    {
        private static readonly RegisterCommandValidation s_validation = new();

        public Guid UserId { get; }
        public string Username { get; }
        public string Email { get; }
        public string Password { get; }
        public string Fullname { get; }
        public string? Bio {  get; }
        public string AvatarUrl { get; }
        public string CoverPhotoUrl { get; }
        public string PhoneNumber { get; }  
        public string? Website { get; }
        public string? Location { get; }
        public string QrUrl { get; }
        public DateTime Birthdate { get; }
        public Gender Gender { get; }

        public RegisterCommand(
            Guid userId,
            string username,
            string email,
            string password,
            string fullname,
            string? bio,
            string avatarUrl,
            string coverPhotoUrl,
            string phoneNumber,
            string? website,
            string? location,
            string qrUrl,
            DateTime birthdate,
            Gender gender
        ) : base(userId)
        {
            UserId = userId;
            Username = username;
            Email = email;
            Password = password;
            Fullname = fullname;
            Bio = bio;
            AvatarUrl = avatarUrl;
            CoverPhotoUrl = coverPhotoUrl;
            PhoneNumber = phoneNumber;
            Website = website;
            Location = location;
            QrUrl = qrUrl;
            Birthdate = birthdate;
            Gender = gender;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
