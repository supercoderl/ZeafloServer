using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Entities
{
    public class MapTheme : Entity<Guid>
    {
        [Column("map_theme_id")]
        public Guid MapThemeId { get; private set; }

        [Column("name")]
        public string Name { get; private set; }

        [Column("description")]
        public string? Description { get; private set; }

        [Column("map_style")]
        public string MapStyle {  get; private set; }

        [Column("preview_url")]
        public string PreviewUrl { get; private set; }

        [Column("is_premium")]
        public bool IsPremium { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }

        public MapTheme(
            Guid mapThemeId,
            string name,
            string? description,
            string mapStyle,
            string previewUrl,
            bool isPremium,
            DateTime createdAt
        ) : base(mapThemeId)
        {
            MapThemeId = mapThemeId;
            Name = name;
            Description = description;
            MapStyle = mapStyle;
            PreviewUrl = previewUrl;
            IsPremium = isPremium;
            CreatedAt = createdAt;
        }

        public void SetName( string name ) { Name = name; }
        public void SetDescription( string? description ) { Description = description; }
        public void SetMapStyle( string mapStyle ) { MapStyle = mapStyle; }
        public void SetPreviewUrl( string previewUrl ) { PreviewUrl = previewUrl; }
        public void SetIsPremium( bool isPremium ) { IsPremium = isPremium; }
        public void SetCreatedAt( DateTime createdAt ) { CreatedAt = createdAt; }
    }
}
