using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Entities
{
    public class Place : Entity<Guid>
    {
        [Column("place_id")]
        public Guid PlaceId { get; private set; }

        [Column("name")]
        public string Name { get; private set; }

        [Column("address")]
        public string Address { get; private set; }

        [Column("type")]
        public PlaceType Type { get; private set; }

        [Column("city_id")]
        public Guid CityId { get; private set; }

        [Column("latitude")]
        public double Latitude { get; private set; }

        [Column("longtitude")]
        public double Longitude { get; private set; }

        [Column("rating")]
        public double Rating { get; private set; }

        [Column("review_count")]
        public int ReviewCount { get; private set; }

        [Column("is_open")]
        public bool IsOpen { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; private set; }

        [ForeignKey("CityId")]
        [InverseProperty("Places")]
        public virtual City? City { get; set; }

        [InverseProperty("Place")]
        public virtual ICollection<PlaceImage> PlaceImages { get; set; } = new List<PlaceImage>();

        [InverseProperty("Place")]
        public virtual ICollection<PlaceLike> PlaceLikes { get; set; } = new List<PlaceLike>();

        public Place(
            Guid placeId,
            string name,
            string address,
            PlaceType type,
            Guid cityId,
            double latitude,
            double longitude,
            double rating,
            int reviewCount,
            bool isOpen,
            DateTime createdAt,
            DateTime? updatedAt
        ) : base( placeId )
        {
            PlaceId = placeId;
            Name = name;
            Address = address;
            Type = type;
            CityId = cityId;
            Latitude = latitude;
            Longitude = longitude;
            Rating = rating;
            ReviewCount = reviewCount;
            IsOpen = isOpen;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public void SetName( string name ) { Name = name; }
        public void SetAddress(string address) { Address = address; }
        public void SetType(PlaceType type) { Type = type; }
        public void SetCityId( Guid cityId ) { CityId = cityId; }
        public void SetLatitude( double latitude ) { Latitude = latitude; }
        public void SetLongitude( double longitude ) { Longitude = longitude; }
        public void SetRating( double rating ) { Rating = rating; }
        public void SetReviewCount( int reviewCount ) { ReviewCount = reviewCount; }
        public void SetIsOpen( bool isOpen ) { IsOpen = isOpen; }
        public void SetCreatedAt( DateTime created_at ) { CreatedAt = created_at; }
        public void SetUpdatedAt( DateTime? updated_at ) { UpdatedAt = updated_at; }
    }
}
