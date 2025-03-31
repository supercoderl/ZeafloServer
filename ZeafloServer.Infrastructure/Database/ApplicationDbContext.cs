using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Infrastructure.Configurations;

namespace ZeafloServer.Infrastructure.Database
{
    public partial class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Token> Tokens { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<FriendShip> FriendShips { get; set; } = null!;
        public DbSet<MapTheme> MapThemes { get; set; } = null!;
        public DbSet<City> Citys { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Like> Likes { get; set; } = null!;
        public DbSet<MemberShipLevel> MemberShipLevels { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<Place> Places { get; set; } = null!;
        public DbSet<PlaceImage> PlacesImage { get; set; } = null!;
        public DbSet<PlaceLike> PlacesLike { get; set; } = null!;
        public DbSet<Report> Reports { get; set; } = null!;
        public DbSet<SavePost> SavePosts { get; set; } = null!;
        public DbSet<StoryActivity> StoryActivities { get; set; } = null!;
        public DbSet<UserLevel> UserLevels { get; set; } = null!;
        public DbSet<UserStatus> UserStatuses { get; set; } = null!;
        public DbSet<PostReaction> PostReactions { get; set; } = null!;
        public DbSet<PostMedia> PostMedia { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.ClrType.GetProperty(DbContextUtility.IsDeleteProperty) is not null)
                {
                    modelBuilder.Entity(entity.ClrType).HasQueryFilter(DbContextUtility.GetIsDeletedRestriction(entity.ClrType));
                }
            }

            base.OnModelCreating(modelBuilder);

            ApplyConfigurations(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TokenConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new FriendShipConfiguration());
            modelBuilder.ApplyConfiguration(new MapThemeConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new LikeConfiguration());
            modelBuilder.ApplyConfiguration(new MemberShipLevelConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new PasswordResetTokenConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new PlaceConfiguration());
            modelBuilder.ApplyConfiguration(new PlaceLikeConfiguration());
            modelBuilder.ApplyConfiguration(new PlaceImageConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            modelBuilder.ApplyConfiguration(new SavePostConfiguration());
            modelBuilder.ApplyConfiguration(new StoryActivityConfiguration());
            modelBuilder.ApplyConfiguration(new UserLevelConfiguration());
            modelBuilder.ApplyConfiguration(new UserStatusConfiguration());
            modelBuilder.ApplyConfiguration(new PostMediaConfiguration());
            modelBuilder.ApplyConfiguration(new PostReactionConfiguration());
        }
    }
}
