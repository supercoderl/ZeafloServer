using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Application.ViewModels.Cities
{
    public sealed record CreateCityRequest
    (
        string Name,
        string PostalCode,
        double Latitude,
        double Longitude
    );
}
