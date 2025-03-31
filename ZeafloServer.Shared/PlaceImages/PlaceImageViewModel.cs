using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.PlaceImages
{
    public sealed record PlaceImageViewModel(
         Guid PlaceImageId,
         Guid PlaceId,
         string ImageUrl
    );
}
