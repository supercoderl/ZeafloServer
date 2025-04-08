using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Application.ViewModels.PlaceImages
{
    public sealed record CreatePlaceImageRequest
    (
        Guid PlaceId,
        string ImageUrl
    );
}
