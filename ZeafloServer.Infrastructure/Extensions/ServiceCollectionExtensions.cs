using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Infrastructure.Database;
using ZeafloServer.Infrastructure.EventSourcing;
using ZeafloServer.Infrastructure.Repositories;

namespace ZeafloServer.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration,
            string migrationsAssemblyName,
            string connectionStringName = "DefaultConnection"
        )
        {
            services.AddDbContext<EventStoreDbContext>(
                options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString(connectionStringName),
                    b => b.MigrationsAssembly(migrationsAssemblyName));
                }
            );

            services.AddDbContext<DomainNotificationStoreDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(connectionStringName),
                b => b.MigrationsAssembly(migrationsAssemblyName));
            }
            );


            //Core
            services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();
            services.AddScoped<IEventStoreContext, EventStoreContext>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<Domain.DomainEvents.IDomainEventStore, EventStore>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            //Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IFriendShipRepository, FriendShipRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IMapThemeRepository, MapThemeRepository>();
            services.AddScoped<IMemberShipLevelRepository, MemberShipLevelRepository>();    
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IPlaceImageRepository, PlaceImageRepository>();
            services.AddScoped<IPlaceLikeRepository, PlaceLikeRepository>();
            services.AddScoped<IPlaceRepository, PlaceRepository>();    
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<ISavePostRepository, SavePostRepository>();
            services.AddScoped<IStoryActivityRepository, StoryActivityRepository>();
            services.AddScoped<IUserLevelRepository, UserLevelRepository>();
            services.AddScoped<IUserStatusRepository, UserStatusRepository>();
            services.AddScoped<IPostMediaRepository, PostMediaRepository>();
            services.AddScoped<IPostReactionRepository, PostReactionRepository>();
            services.AddScoped<IProcessingRepository, ProcessingRepository>();

            return services;
        }
    }
}
