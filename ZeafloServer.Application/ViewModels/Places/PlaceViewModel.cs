using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.ViewModels.Places
{
    public sealed class PlaceViewModel
    {
        public Guid PlaceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Description { get; set; }
        public PlaceType Type { get; set; } 
        public Guid CityId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Rating { get; set; }
        public int ReviewCount { get; set; }
        public bool IsOpen { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int FavoriteCount { get; set; }
        public string Location { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        public static PlaceViewModel FromPlace(Place place)
        {
            return new PlaceViewModel
            {
                PlaceId = place.Id,
                Name = place.Name,
                Address = place.Address,
                Description = place.Description,
                Type = place.Type,
                CityId = place.CityId,
                Latitude = place.Latitude,
                Longitude = place.Longitude,
                Rating = place.Rating,
                ReviewCount = place.ReviewCount,
                IsOpen = place.IsOpen,
                CreatedAt = place.CreatedAt,
                UpdatedAt = place.UpdatedAt,
                FavoriteCount = place.PlaceLikes.Count,
                Location = place.City?.Name ?? string.Empty,
                ImageUrl = place.PlaceImages.Any() ? place.PlaceImages.First().ImageUrl : null
            };
        }
    }
}
