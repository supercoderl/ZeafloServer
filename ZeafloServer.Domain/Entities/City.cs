using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Entities
{
    public class City : Entity<Guid>
    {
        [Column("city_id")]
        public Guid CityId { get; private set; }

        [Column("name")]
        public string Name { get; private set; }

        [Column("postal_code")]
        public string PostalCode { get; private set; }

        [Column("latitude")]
        public double Latitude { get; private set; }

        [Column("longtitude")]
        public double Longitude { get; private set; }

        [InverseProperty("City")]
        public virtual ICollection<Place> Places { get; set; } = new List<Place>();

        [InverseProperty("City")]
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

        public City(
            Guid cityId,
            string name,
            string postalCode,
            double latitude,
            double longitude
        ) : base(cityId)
        {
            CityId = cityId;
            Name = name;
            PostalCode = postalCode;
            Latitude = latitude;
            Longitude = longitude;
        }

        public void SetName( string name ) { Name = name; } 
        public void SetPostalCode( string postalCode ) { PostalCode = postalCode; }
        public void SetLatitude( double latitude ) { Latitude = latitude; }
        public void SetLongitude( double longitude ) { Longitude = longitude; }
    }
}
