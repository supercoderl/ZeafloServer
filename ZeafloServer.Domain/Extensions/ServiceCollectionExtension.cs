using CloudinaryDotNet;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Commands.Cities.CreateCity;
using ZeafloServer.Domain.Commands.Files.DeleteFile;
using ZeafloServer.Domain.Commands.Files.UploadFile;
using ZeafloServer.Domain.Commands.FriendShips.AcceptRequest;
using ZeafloServer.Domain.Commands.FriendShips.AddFriend;
using ZeafloServer.Domain.Commands.FriendShips.CancelRequest;
using ZeafloServer.Domain.Commands.MapThemes.CreateMapTheme;
using ZeafloServer.Domain.Commands.MemberShipLevels.CreateMemberShipLevel;
using ZeafloServer.Domain.Commands.Messages.CreateMessage;
using ZeafloServer.Domain.Commands.Messages.UpdateUnreadMessage;
using ZeafloServer.Domain.Commands.PhotoPosts.CreatePhotoPost;
using ZeafloServer.Domain.Commands.PlaceImages.CreatePlaceImage;
using ZeafloServer.Domain.Commands.Places.CreatePlace;
using ZeafloServer.Domain.Commands.Places.ImportPlace;
using ZeafloServer.Domain.Commands.Places.ReactPlace;
using ZeafloServer.Domain.Commands.Plans.CreatePlan;
using ZeafloServer.Domain.Commands.Points.AddPoint;
using ZeafloServer.Domain.Commands.PostMedias.CreatePostMedia;
using ZeafloServer.Domain.Commands.Posts.CreatePost;
using ZeafloServer.Domain.Commands.Posts.ReactPost;
using ZeafloServer.Domain.Commands.Posts.SavePost;
using ZeafloServer.Domain.Commands.Processes.CreateProcess;
using ZeafloServer.Domain.Commands.Processes.UpdateProcess;
using ZeafloServer.Domain.Commands.StoryActivities.LogActivity;
using ZeafloServer.Domain.Commands.Tokens.CreateToken;
using ZeafloServer.Domain.Commands.Tokens.UpdateToken;
using ZeafloServer.Domain.Commands.TripDurations.CreateTripDuration;
using ZeafloServer.Domain.Commands.Users.ChangePassword;
using ZeafloServer.Domain.Commands.Users.ChangeUserAvatar;
using ZeafloServer.Domain.Commands.Users.ForgotPassword;
using ZeafloServer.Domain.Commands.Users.Login;
using ZeafloServer.Domain.Commands.Users.RefreshToken;
using ZeafloServer.Domain.Commands.Users.Register;
using ZeafloServer.Domain.Commands.Users.ResetPassword;
using ZeafloServer.Domain.Commands.Users.RetrieveQr;
using ZeafloServer.Domain.Commands.Users.UpdateUser;
using ZeafloServer.Domain.Consumers;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.EventHandler.Fanout;
using ZeafloServer.Domain.Helpers;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Settings;

namespace ZeafloServer.Domain.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            //User
            services.AddScoped<IRequestHandler<LoginCommand, object?>, LoginCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterCommand, Guid>, RegisterCommandHandler>();
/*            services.AddScoped<IRequestHandler<ForgotPasswordCommand, string>, ForgotPasswordCommandHandler>();*/
            services.AddScoped<IRequestHandler<ResetPasswordCommand, bool>,  ResetPasswordCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand, User?>,  UpdateUserCommandHandler>();
            services.AddScoped<IRequestHandler<ChangePasswordCommand, bool>, ChangePasswordCommandHandler>();
            services.AddScoped<IRequestHandler<RefreshTokenCommand, object?>, RefreshTokenCommandHandler>();
            services.AddScoped<IRequestHandler<RetrieveQrCommand, string>, RetrieveQrCommandHandler>();
            services.AddScoped<IRequestHandler<ChangeUserAvatarCommand, string>, ChangeUserAvatarCommandHandler>();

            //Token
            services.AddScoped<IRequestHandler<CreateTokenCommand, Guid>, CreateTokenCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateTokenCommand, Guid>, UpdateTokenCommandHandler>();

            //File
            services.AddScoped<IRequestHandler<UploadFileCommand, string>, UploadFileCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteFileCommand, bool>, DeleteFileCommandHandler>();

            //Post
            services.AddScoped<IRequestHandler<CreatePostCommand, Guid>, CreatePostCommandHandler>();

            //FriendShip
            services.AddScoped<IRequestHandler<AddFriendCommand, Guid>, AddFriendCommandHandler>();
            services.AddScoped<IRequestHandler<CancelRequestCommand, bool>, CancelRequestCommandHandler>();
            services.AddScoped<IRequestHandler<AcceptRequestCommand, bool>, AcceptRequestCommandHandler>();

            //PostMedia
            services.AddScoped<IRequestHandler<CreatePostMediaCommand, Guid>, CreatePostMediaCommandHandler>();

            //MapTheme
            services.AddScoped<IRequestHandler<CreateMapThemeCommand, Guid>, CreateMapThemeCommandHandler>();

            //Message
            services.AddScoped<IRequestHandler<CreateMessageCommand, Message?>, CreateMessageCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUnreadMessageCommand, bool>, UpdateUnreadMessageCommandHandler>();

            //Post Reaction
            services.AddScoped<IRequestHandler<ReactPostCommand, Guid>, ReactPostCommandHandler>();

            //Save Post
            services.AddScoped<IRequestHandler<SavePostCommand, Guid>, SavePostCommandHandler>();

            //Membership Level
            services.AddScoped<IRequestHandler<CreateMemberShipLevelCommand, Guid>, CreateMemberShipLevelCommandHandler>();

            //Process
            services.AddScoped<IRequestHandler<CreateProcessCommand, Processing?>, CreateProcessCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProcessCommand, Processing?>, UpdateProcessCommandHandler>();

            //Place
            services.AddScoped<IRequestHandler<CreatePlaceCommand, Guid>, CreatePlaceCommandHandler>();
            services.AddScoped<IRequestHandler<ReactPlaceCommand, Guid>, ReactPlaceCommandHandler>();
            services.AddScoped<IRequestHandler<ImportPlaceCommand, List<Guid>>, ImportPlaceCommandHandler>();

            //Place Image
            services.AddScoped<IRequestHandler<CreatePlaceImageCommand, Guid>, CreatePlaceImageCommandHandler>();

            //City
            services.AddScoped<IRequestHandler<CreateCityCommand, Guid>, CreateCityCommandHandler>();

            //Trip Duration 
            services.AddScoped<IRequestHandler<CreateTripDurationCommand, Guid>, CreateTripDurationCommandHandler>();

            //Photo Post
            services.AddScoped<IRequestHandler<CreatePhotoPostCommand, Guid>, CreatePhotoPostCommandHandler>();

            //User Level
            services.AddScoped<IRequestHandler<AddPointCommand, Guid>, AddPointCommandHandler>();

            //Story Activity
            services.AddScoped<IRequestHandler<LogActivityCommand, Guid>, LogActivityCommandHandler>();

            //Plan
            services.AddScoped<IRequestHandler<CreatePlanCommand, Guid>, CreatePlanCommandHandler>();

            return services;
        }
        public static IServiceCollection AddNotificationHandlers(this IServiceCollection services)
        {
            //Fanout
            services.AddScoped<IFanoutEventHandler, FanoutEventHandler>();

            return services;
        }

        public static IServiceCollection AddApiUser(this IServiceCollection services)
        {
            //User
            services.AddScoped<IUser, ApiUser>();

            return services;
        }

        public static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            services.AddSingleton<TokenHelpers>();
            services.AddSingleton<QRCodeHelpers>();
            services.AddSingleton<UploadHelpers>();
            services.AddSingleton<ProcessingHelpers>();
            services.AddSingleton<FileHelpers>();

            return services;
        }

        public static IServiceCollection AddAccounts(this IServiceCollection services)
        {
            services.AddSingleton<Cloudinary>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var cloudinaryAccount = new Account(
                    configuration["CloudinaryConfiguration:CloudName"],
                    configuration["CloudinaryConfiguration:ApiKey"],
                    configuration["CloudinaryConfiguration:ApiSecret"]
                );
                return new Cloudinary(cloudinaryAccount);
            });

            return services;
        }

        public static IServiceCollection AddConsumers(this IServiceCollection services, RabbitMqConfiguration rabbitConfiguration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<FanoutEventConsumer>();
                x.AddConsumer<GenerateQRCodeConsumer>();
                x.AddConsumer<UploadConsumer>();
                x.AddConsumer<GainPointConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ConfigureNewtonsoftJsonSerializer(settings =>
                    {
                        settings.TypeNameHandling = TypeNameHandling.Objects;
                        settings.NullValueHandling = NullValueHandling.Ignore;
                        return settings;
                    });
                    cfg.UseNewtonsoftJsonSerializer();
                    cfg.ConfigureNewtonsoftJsonDeserializer(settings =>
                    {
                        settings.TypeNameHandling = TypeNameHandling.Objects;
                        settings.NullValueHandling = NullValueHandling.Ignore;
                        return settings;
                    });

                    cfg.Host(rabbitConfiguration.Host, (ushort)rabbitConfiguration.Port, "/", h => {
                        h.Username(rabbitConfiguration.Username);
                        h.Password(rabbitConfiguration.Password);
                    });

                    // Every instance of the service will receive the message
                    cfg.ReceiveEndpoint("zeaflo-fanout-event-" + Guid.NewGuid(), e =>
                    {
                        e.Durable = false;
                        e.AutoDelete = true;
                        e.ConfigureConsumer<FanoutEventConsumer>(context);
                        e.DiscardSkippedMessages();
                    });

                    cfg.ReceiveEndpoint("zeaflo-qr-code-queue", e =>
                    {
                        e.ConfigureConsumer<GenerateQRCodeConsumer>(context);
                        e.DiscardSkippedMessages();
                    });

                    cfg.ReceiveEndpoint("zeaflo-upload-queue", e =>
                    {
                        e.ConfigureConsumer<UploadConsumer>(context);
                        e.DiscardSkippedMessages();
                    });

                    cfg.ReceiveEndpoint("zeaflo-gain-point-queue", e =>
                    {
                        e.ConfigureConsumer<GainPointConsumer>(context);
                        e.DiscardSkippedMessages();
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
