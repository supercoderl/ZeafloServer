using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Contexts;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Grpc.Models;
using ZeafloServer.Proto.Users;

namespace ZeafloServer.Grpc.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGrpcClient(
            this IServiceCollection services,
            IConfiguration configuration,
            string configSectionKey = "Grpc")
        {
            var settings = new GRPCSettings();
            configuration.Bind(configSectionKey, settings);

            return AddGrpcClient(services, settings);
        }

        public static IServiceCollection AddGrpcClient(
            this IServiceCollection services,
            GRPCSettings settings)
        {
            if (!string.IsNullOrWhiteSpace(settings.ZeafloUrl))
            {
                services.AddZeafloGrpcClient(settings.ZeafloUrl);
            }

            services.AddSingleton<IZeaflo, Zeaflo>();

            return services;
        }

        public static IServiceCollection AddZeafloGrpcClient(
            this IServiceCollection services,
            string grpcUrl)
        {
            if (string.IsNullOrWhiteSpace(grpcUrl))
            {
                return services;
            }

            var channel = GrpcChannel.ForAddress(grpcUrl);

            var usersClient = new UsersApi.UsersApiClient(channel);
            services.AddSingleton(usersClient);

            services.AddSingleton<IUsersContext, UsersContext>();
            services.AddSingleton<ICitiesContext, CitiesContext>();
            services.AddSingleton<ICommentsContext, CommentsContext>();
            services.AddSingleton<IFriendShipsContext, FriendShipsContext>();
            services.AddSingleton<ILikesContext, LikesContext>();
            services.AddSingleton<IMapThemesContext, MapThemesContext>();
            services.AddSingleton<IMemberShipLevelsContext, MemberShipLevelsContext>();
            services.AddSingleton<IMessagesContext, MessagesContext>();
            services.AddSingleton<INotificationsContext, NotificationsContext>();
            services.AddSingleton<IPasswordResetTokensContext, PasswordResetTokensContext>();
            services.AddSingleton<IPlacesContext, PlacesContext>();
            services.AddSingleton<IPlaceImagesContext, PlaceImagesContext>();
            services.AddSingleton<IPlaceLikesContext, PlaceLikesContext>(); 
            services.AddSingleton<IPostsContext, PostsContext>();
            services.AddSingleton<IReportsContext, ReportsContext>();
            services.AddSingleton<ISavePostsContext, SavePostsContext>();
            services.AddSingleton<IStoryActivitiesContext, StoryActivitiesContext>();
            services.AddSingleton<ITokensContext, TokensContext>();
            services.AddSingleton<IUserLevelsContextcs, UserLevelsContext>();
            services.AddSingleton<IUserStatusesContext, UserStatusesContext>();
            services.AddSingleton<IPhotoPostsContext, PhotoPostsContext>();

            return services;
        }
    }
}
