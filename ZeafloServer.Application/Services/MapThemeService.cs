using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.Queries.MapThemes.GetAll;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Application.ViewModels.MapThemes;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Domain.Commands.MapThemes.CreateMapTheme;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Application.Services
{
    public class MapThemeService : IMapThemeService
    {
        private readonly IMediatorHandler _bus;

        public MapThemeService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<Guid> CreateMapThemeAsync(CreateMapThemeRequest request)
        {
            return await _bus.SendCommandAsync(new CreateMapThemeCommand(
                Guid.NewGuid(),
                request.Name,
                request.Description,
                request.MapStyle,
                request.PreviewUrl,
                request.IsPremium
            ));
        }

        public async Task<PageResult<MapThemeViewModel>> GetAllMapThemesAsync(PageQuery query, ActionStatus status, string searchTerm = "", SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllMapThemesQuery(query, status, searchTerm, sortQuery));
        }
    }
}
