using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.ViewModels.Places
{
    public sealed record CreatePlaceRequest
    (
        string Name,
        string Address,
        string? Description,
        PlaceType Type,
        Guid CityId,
        double Latitude,
        double Logitude,
        double Rating,
        int ReviewCount,
        bool IsOpen
    );
}
