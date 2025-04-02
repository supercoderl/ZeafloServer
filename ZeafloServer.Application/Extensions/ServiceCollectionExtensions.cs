using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.Queries.Cities.GetAll;
using ZeafloServer.Application.Queries.Cities.GetById;
using ZeafloServer.Application.Queries.Comments.GetAll;
using ZeafloServer.Application.Queries.FriendShips.GetAll;
using ZeafloServer.Application.Queries.FriendShips.GetContacts;
using ZeafloServer.Application.Queries.Likes.GetAll;
using ZeafloServer.Application.Queries.MapThemes.GetAll;
using ZeafloServer.Application.Queries.MemberShipLevels.GetAll;
using ZeafloServer.Application.Queries.Messages.GetAll;
using ZeafloServer.Application.Queries.Notifications.GetAll;
using ZeafloServer.Application.Queries.Places.GetAll;
using ZeafloServer.Application.Queries.Posts.GetAll;
using ZeafloServer.Application.Queries.Users.GetAll;
using ZeafloServer.Application.Queries.Users.GetById;
using ZeafloServer.Application.Queries.Users.GetLevel;
using ZeafloServer.Application.Services;
using ZeafloServer.Application.SortProviders;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Cities;
using ZeafloServer.Application.ViewModels.Comments;
using ZeafloServer.Application.ViewModels.FriendShips;
using ZeafloServer.Application.ViewModels.Likes;
using ZeafloServer.Application.ViewModels.MapThemes;
using ZeafloServer.Application.ViewModels.MemberShipLevels;
using ZeafloServer.Application.ViewModels.Messages;
using ZeafloServer.Application.ViewModels.Notifications;
using ZeafloServer.Application.ViewModels.Places;
using ZeafloServer.Application.ViewModels.Posts;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels.UserLevels;
using ZeafloServer.Application.ViewModels.Users;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDeepseekService, DeepseekService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IFriendShipService, FriendShipService>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<IMapThemeService, MapThemeService>();
            services.AddScoped<IMemberShipLevelService, MemberShipLevelService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostMediaService, PostMediaService>();
            services.AddScoped<IUserLevelService, UserLevelService>();

            return services;
        }

        public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        {
            //City
            services.AddScoped<IRequestHandler<GetAllCitiesQuery, PageResult<CityViewModel>>, GetAllCitiesQueryHandler>();
            services.AddScoped<IRequestHandler<GetCityByIdQuery, CityViewModel?>, GetCityByIdQueryHandler>();

            //Comment
            services.AddScoped<IRequestHandler<GetAllCommentsQuery, PageResult<CommentViewModel>>, GetAllCommentsQueryHandler>();

            //FriendShip
            services.AddScoped<IRequestHandler<GetAllFriendShipsQuery, PageResult<FriendShipViewModel>>, GetAllFriendShipsQueryHandler>();

            // Like
            services.AddScoped<IRequestHandler<GetAllLikesQuery, PageResult<LikeViewModel>>, GetAllLikesQueryHandler>();

            // MapTheme
            services.AddScoped<IRequestHandler<GetAllMapThemesQuery, PageResult<MapThemeViewModel>>, GetAllMapThemesQueryHandler>();

            // MemberShipLevel
            services.AddScoped<IRequestHandler<GetAllMemberShipLevelsQuery, PageResult<MemberShipLevelViewModel>>, GetAllMemberShipLevelsQueryHandler>();

            // Message
            services.AddScoped<IRequestHandler<GetAllMessagesQuery, PageResult<MessageViewModel>>, GetAllMessagesQueryHandler>();

            // Notification
            services.AddScoped<IRequestHandler<GetAllNotificationsQuery, PageResult<NotificationViewModel>>, GetAllNotificationsQueryHandler>();

            // Place
            services.AddScoped<IRequestHandler<GetAllPlacesQuery, PageResult<PlaceViewModel>>, GetAllPlacesQueryHandler>();

            // Post
            services.AddScoped<IRequestHandler<GetAllPostsQuery, PageResult<PostViewModel>>, GetAllPostsQueryHandler>();

            // User
            services.AddScoped<IRequestHandler<GetAllUsersQuery, PageResult<UserViewModel>>, GetAllUsersQueryHandler>();
            services.AddScoped<IRequestHandler<GetUserByIdQuery, UserViewModel?>, GetUserByIdQueryHandler>();
            services.AddScoped<IRequestHandler<GetContactsQuery, PageResult<ContactInfo>>, GetContactsQueryHandler>();

            // UserLevel
            services.AddScoped<IRequestHandler<GetUserLevelQuery, UserLevelViewModel?>, GetUserLevelQueryHandler>();

            return services;
        }

        public static IServiceCollection AddSortProviders(this IServiceCollection services)
        {
            services.AddScoped<ISortingExpressionProvider<CityViewModel, City>, CityViewModelSortProvider>();
            services.AddScoped<ISortingExpressionProvider<CommentViewModel, Comment>, CommentViewModelSortProvider>();
            services.AddScoped<ISortingExpressionProvider<FriendShipViewModel, FriendShip>, FriendShipViewModelSortProvider>();
            services.AddScoped<ISortingExpressionProvider<LikeViewModel, Like>, LikeViewModelSortProvider>();
            services.AddScoped<ISortingExpressionProvider<MapThemeViewModel, MapTheme>, MapThemeViewModelSortProvider>();
            services.AddScoped<ISortingExpressionProvider<MemberShipLevelViewModel, MemberShipLevel>,  MemberShipLevelViewModelSortProvider>();
            services.AddScoped<ISortingExpressionProvider<MessageViewModel, Message>, MessageViewModelSortProvider>();
            services.AddScoped<ISortingExpressionProvider<NotificationViewModel, Notification>, NotificationViewModelSortProvider>();
            services.AddScoped<ISortingExpressionProvider<PlaceViewModel, Place>, PlaceViewModelSortProvider>();    
            services.AddScoped<ISortingExpressionProvider<PostViewModel, Post>, PostViewModelSortProvider>();
            services.AddScoped<ISortingExpressionProvider<UserViewModel, User>, UserViewModelSortProvider>();

            return services;
        }
    }
}
