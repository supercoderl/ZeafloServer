using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.ViewModels.MapThemes
{
    public sealed class MapThemeViewModel
    {
        public Guid MapThemeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description {  get; set; }
        public string MapStyle { get; set; } = string.Empty;
        public string PreviewUrl {  get; set; } = string.Empty;
        public bool IsPremium { get; set; }
        public DateTime CreatedAt { get; set; }

        public static MapThemeViewModel FromMapTheme(MapTheme mapTheme)
        {
            return new MapThemeViewModel
            {
                MapThemeId = mapTheme.Id,
                Name = mapTheme.Name,
                Description = mapTheme.Description,
                MapStyle = mapTheme.MapStyle,
                PreviewUrl = mapTheme.PreviewUrl,
                CreatedAt = mapTheme.CreatedAt,
                IsPremium = mapTheme.IsPremium,
            };
        }
    }
}
