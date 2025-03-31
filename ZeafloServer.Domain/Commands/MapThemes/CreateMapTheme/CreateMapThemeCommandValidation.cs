using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Errors;

namespace ZeafloServer.Domain.Commands.MapThemes.CreateMapTheme
{
    public sealed class CreateMapThemeCommandValidation : AbstractValidator<CreateMapThemeCommand>
    {
        public CreateMapThemeCommandValidation()
        {
            RuleForName();
            RuleForMapStyle();
            RuleForPreviewUrl();
        }

        private void RuleForName()
        {
            RuleFor(cmd => cmd.Name).NotEmpty().WithErrorCode(DomainErrorCodes.MapTheme.EmptyName).WithMessage("Name may not be empty.");
        }

        private void RuleForMapStyle()
        {
            RuleFor(cmd => cmd.MapStyle).NotEmpty().WithErrorCode(DomainErrorCodes.MapTheme.EmptyMapStyle).WithMessage("Map style may not be empty.");
        }

        private void RuleForPreviewUrl()
        {
            RuleFor(cmd => cmd.PreviewUrl).NotEmpty().WithErrorCode(DomainErrorCodes.MapTheme.EmptyPreviewUrl).WithMessage("Preview url may not be empty.");
        }
    }
}
