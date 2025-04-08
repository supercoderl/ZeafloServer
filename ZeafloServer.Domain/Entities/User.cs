using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Entities
{
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User : Entity<Guid>
    {
        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("username")]
        public string Username { get; private set; }

        [Column("email")]
        public string Email { get; private set; }

        [Column("password_hash")]
        public string PasswordHash { get; private set; }

        [Column("full_name")]
        public string Fullname { get; private set; }

        [Column("bio")]
        public string? Bio {  get; private set; }

        [Column("avatar_url")]
        public string AvatarUrl { get; private set; }

        [Column("cover_photo_url")]
        public string CoverPhotoUrl { get; private set; }

        [Column("phone_number")]
        public string PhoneNumber { get; private set; }

        [Column("website")]
        public string? Website {  get; private set; }

        [Column("location")]
        public string? Location { get; private set; }

        [Column("qr_url")]
        public string QrUrl { get; private set; }

        [Column("birth_date")]
        public DateTime BirthDate { get; private set; }

        [Column("gender")]
        public Gender Gender { get; private set; }

        [Column("is_online")]
        public bool IsOnline { get; private set; }

        [Column("last_login_time")]
        public DateTime? LastLoginTime { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; private set; }

        [InverseProperty("User")]
        public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();

        [InverseProperty("User")]
        public virtual ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = new List<PasswordResetToken>();

        [InverseProperty("User")]
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

        [InverseProperty("User")]
        public virtual ICollection<FriendShip> FriendShips { get; set; } = new List<FriendShip>();

        [InverseProperty("Friend")]
        public virtual ICollection<FriendShip> Friends { get; set; } = new List<FriendShip>();

        [InverseProperty("User")]
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        [InverseProperty("User")]
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

        [InverseProperty("Sender")]
        public virtual ICollection<Message> SenderMessages { get; set; } = new List<Message>();

        [InverseProperty("Receiver")]
        public virtual ICollection<Message> ReceiverMessages { get; set; } = new List<Message>();

        [InverseProperty("User")]
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        [InverseProperty("User")]
        public virtual ICollection<PlaceLike> PlaceLikes { get; set; } = new List<PlaceLike>();

        [InverseProperty("User")]
        public virtual ICollection<SavePost> SavePosts { get; set; } = new List<SavePost>();

        [InverseProperty("User")]
        public virtual ICollection<UserStatus> UserStatuses { get; set; } = new List<UserStatus>();

        [InverseProperty("User")]
        public virtual UserLevel? UserLevel { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

        [InverseProperty("User")]
        public virtual ICollection<StoryActivity> StoryActivities { get; set; } = new List<StoryActivity>();

        [InverseProperty("User")]
        public virtual ICollection<PostReaction> PostReactions { get; set; } = new List<PostReaction>();

        [InverseProperty("User")]
        public virtual Processing? Processing { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

        [InverseProperty("User")]
        public virtual ICollection<PhotoPost> PhotoPosts { get; set; } = new List<PhotoPost>();

        public User(
            Guid userId,
            string username,
            string email,
            string passwordHash,
            string fullname,
            string? bio,
            string avatarUrl,
            string coverPhotoUrl,
            string phoneNumber,
            string? website,
            string? location,
            string qrUrl,
            DateTime birthDate,
            Gender gender,
            bool isOnline,
            DateTime? lastLoginTime,
            DateTime createdAt,
            DateTime? updatedAt
        ) : base(userId)
        {
            UserId = userId;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Fullname = fullname;
            Bio = bio;
            AvatarUrl = avatarUrl;
            CoverPhotoUrl = coverPhotoUrl;
            PhoneNumber = phoneNumber;
            Website = website;
            Location = location;
            QrUrl = qrUrl;
            BirthDate = birthDate;
            Gender = gender;
            IsOnline = isOnline;
            LastLoginTime = lastLoginTime;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public void SetUsername( string username ) { Username = username; }
        public void SetEmail( string email ) { Email = email; }
        public void SetPasswordHash( string passwordHash ) { PasswordHash = passwordHash; }
        public void SetFullName( string fullName ) { Fullname = fullName; }
        public void SetBio( string? bio ) { Bio = bio; }
        public void SetAvatarUrl( string avatarUrl ) { AvatarUrl = avatarUrl; }
        public void SetCoverPhotoUrl( string coverPhotoUrl ) { CoverPhotoUrl = coverPhotoUrl; }
        public void SetPhoneNumber( string phoneNumber ) { PhoneNumber = phoneNumber; }
        public void SetWebsite( string? website ) { Website = website; }
        public void SetLocation( string? location ) { Location = location; }
        public void SetQrUrl(string qrUrl) {  QrUrl = qrUrl; }
        public void SetBirthDate( DateTime birthDate ) { BirthDate = birthDate; }
        public void SetGender( Gender gender ) { Gender = gender; }
        public void SetIsOnline(bool isOnline) { IsOnline = isOnline; }
        public void SetLastLoginTime(DateTime? lastLoginTime) { LastLoginTime = lastLoginTime; }
        public void SetCreatedAt( DateTime createdAt ) { CreatedAt = createdAt; }
        public void SetUpdatedAt( DateTime? updatedAt ) { UpdatedAt = updatedAt; }
    }
}
