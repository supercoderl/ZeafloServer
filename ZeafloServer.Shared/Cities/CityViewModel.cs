using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Cities
{
    public sealed record CityViewModel(
        Guid CityId,
        string Name,
        string PostalCode,
        double Latitude,
        double Longitude
    );
}
