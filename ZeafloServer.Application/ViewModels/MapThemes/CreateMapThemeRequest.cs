using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Application.ViewModels.MapThemes
{
    public sealed record CreateMapThemeRequest
    (
        string Name,
        string? Description,
        string MapStyle,
        string PreviewUrl,
        bool IsPremium
    );
}
