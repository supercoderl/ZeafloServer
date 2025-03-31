using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Application.ViewModels.MapThemes;

namespace ZeafloServer.Application.Interfaces
{
    public interface IMapThemeService
    {
        public Task<PageResult<MapThemeViewModel>> GetAllMapThemesAsync(
            PageQuery query,
            ActionStatus status,
            string searchTerm = "",
            SortQuery? sortQuery = null
        );
        public Task<Guid> CreateMapThemeAsync(CreateMapThemeRequest request);
    }
}
