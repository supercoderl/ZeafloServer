using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Likes
{
    public sealed record LikeViewModel(
         Guid LikeId,
         Guid UserId,
         Guid PostId
    );
}
