using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.MapThemes.CreateMapTheme
{
    public sealed class CreateMapThemeCommand : CommandBase<Guid>, IRequest<Guid>
    {
        private static readonly CreateMapThemeCommandValidation s_validation = new();

        public Guid MapThemeId { get; }
        public string Name { get; }
        public string? Description { get; }
        public string MapStyle { get; }
        public string PreviewUrl { get; }
        public bool IsPremium { get; }

        public CreateMapThemeCommand(
            Guid mapThemeId,
            string name,
            string? description,
            string mapStyle,
            string previewUrl,
            bool isPremium
        ) : base(Guid.NewGuid())
        {
            MapThemeId = mapThemeId;
            Name = name;
            Description = description;
            MapStyle = mapStyle;
            PreviewUrl = previewUrl;
            IsPremium = isPremium;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
