using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.ViewModels.Cities
{
    public sealed class CityViewModel
    {
        public Guid CityId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PostalCode {  get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static CityViewModel FromCity(City city)
        {
            return new CityViewModel
            {
                CityId = city.Id,
                Name = city.Name,
                PostalCode = city.PostalCode,
                Latitude = city.Latitude,
                Longitude = city.Longitude,
            };
        }
    }
}
