using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.PlaceLikes
{
    public sealed record PlaceLikeViewModel(
         Guid PlaceLikeId,
         Guid PaceId,
         Guid UserId
    );
}
