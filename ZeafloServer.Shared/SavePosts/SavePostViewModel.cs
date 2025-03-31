using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.SavePosts
{
    public sealed record SavePostViewModel(
         Guid SavePostId,
         Guid UserId,
         Guid PostId
    );
}
