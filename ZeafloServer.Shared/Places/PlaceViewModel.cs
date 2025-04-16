using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Enums;

namespace ZeafloServer.Shared.Places
{
    public sealed record PlaceViewModel(
         Guid PlaceId,
         string Name,
         string Address,
         string? Description,
         PlaceType Type,
         Guid CityId,
         double Latitude,
         double Longitude,
         double Rating,
         int ReviewCount,
         bool IsOpen
    );
}
