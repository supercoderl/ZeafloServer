using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.MapThemes
{
    public sealed record MapThemeViewModel(
         Guid MapThemeId,
         string Name,
         string? Description,
         string MapStyle,
         string PreviewUrl,
         bool IsPremium
    );
}
