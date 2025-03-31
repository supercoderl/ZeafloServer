using MassTransit;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.Queries.Users.GetAll;
using ZeafloServer.Application.Queries.Users.GetById;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels.Users;
using ZeafloServer.Domain.Commands.Users.ChangePassword;
using ZeafloServer.Domain.Commands.Users.Login;
using ZeafloServer.Domain.Commands.Users.RefreshToken;
using ZeafloServer.Domain.Commands.Users.Register;
using ZeafloServer.Domain.Commands.Users.UpdateUser;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Notifications;
using ZeafloServer.Shared.Events;

namespace ZeafloServer.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMediatorHandler _bus;
        private readonly IWebHostEnvironment _web;
        private readonly IPublishEndpoint _publishEnpoint;

        public UserService(
            IMediatorHandler bus, 
            IWebHostEnvironment web,
            IPublishEndpoint publishEnpoint
        )
        {
            _bus = bus;
            _web = web;
            _publishEnpoint = publishEnpoint;
        }

        public async Task<Guid> RegisterAsync(RegisterViewModel viewModel, string logoPath)
        {
            Guid userId = await _bus.SendCommandAsync(new RegisterCommand(
                Guid.NewGuid(),
                viewModel.Username,
                viewModel.Email,
                viewModel.Password,
                viewModel.Fullname,
                viewModel.Bio,
                viewModel.AvatarUrl,
                viewModel.CoverPhotoUrl,
                viewModel.PhoneNumber,
                viewModel.Website,
                viewModel.Location,
                string.Empty,
                viewModel.Birthdate,
                viewModel.Gender
            ));

            if (userId != Guid.Empty)
            {
                await _publishEnpoint.Publish(new GenerateQRCodeMessageEvent(userId));
            }

            return userId;
        }

        public async Task<object?> LoginAsync(LoginViewModel viewModel)
        {
            return await _bus.SendCommandAsync(new LoginCommand(viewModel.Identifier, viewModel.Password));
        }

        public async Task<PageResult<UserViewModel>> GetAllUsersAsync(PageQuery query, ActionStatus status, string searchTerm = "", SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllUsersQuery(query, status, searchTerm, sortQuery));
        }

        public async Task<UserViewModel?> UpdateUserAsync(UpdateUserRequest request)
        {
            var user = await _bus.SendCommandAsync(new UpdateUserCommand(
                request.UserId,
                request.Username,
                request.Email,
                request.Fullname,
                request.Bio,
                request.AvatarUrl,
                request.CoverPhotoUrl,
                request.PhoneNumber,
                request.Website,
                request.Location,
                request.QrUrl,
                request.Gender,
                request.IsOnline,
                request.LastLoginTime
            ));

            if (user == null) return null;

            return UserViewModel.FromUser(user);
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordRequest request)
        {
            return await _bus.SendCommandAsync(new ChangePasswordCommand(
                request.UserId,
                request.OldPassword,
                request.NewPassword
            ));
        }

        public async Task<UserViewModel?> GetProfileAsync(Guid userId)
        {
            return await _bus.QueryAsync(new GetUserByIdQuery(userId));
        }

        public async Task<object?> RefreshTokenAsync(RefreshTokenRequest request)
        {
            return await _bus.SendCommandAsync(new RefreshTokenCommand(request.RefreshToken));
        }
    }
}
