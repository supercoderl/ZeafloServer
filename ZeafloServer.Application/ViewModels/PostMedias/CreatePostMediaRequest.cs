using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.ViewModels.PostMedias
{
    public sealed record CreatePostMediaRequest
    (
        Guid PostId,
        string MediaUrl,
        MediaType MediaType
    );
}
