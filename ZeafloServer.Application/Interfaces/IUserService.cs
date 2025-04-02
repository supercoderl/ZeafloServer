using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.Users;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.Interfaces
{
    public interface IUserService
    {
        public Task<PageResult<UserViewModel>> GetAllUsersAsync(
            PageQuery query,
            ActionStatus status,
            string searchTerm = "",
            SortQuery? sortQuery = null
        );
        public Task<Guid> RegisterAsync(RegisterViewModel viewModel, string logoPath);
        public Task<object?> LoginAsync(LoginViewModel viewModel);
        public Task<UserViewModel?> UpdateUserAsync(UpdateUserRequest request);
        public Task<bool> ChangePasswordAsync(ChangePasswordRequest request);
        public Task<UserViewModel?> GetProfileAsync(Guid userId);
        public Task<object?> RefreshTokenAsync(RefreshTokenRequest request);
        public Task<string> RetrieveQrAsync(Guid userId);
    }
}
