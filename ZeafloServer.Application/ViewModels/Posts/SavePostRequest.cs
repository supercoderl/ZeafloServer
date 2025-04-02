using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Application.ViewModels.Posts
{
    public sealed record SavePostRequest
    (
        Guid UserId,
        Guid PostId
    );
}
