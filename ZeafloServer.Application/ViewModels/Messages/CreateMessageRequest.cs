using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Application.ViewModels.Messages
{
    public sealed record CreateMessageRequest
    (
        Guid SenderId,
        Guid ReceiverId,
        string Content,
        string? MediaUrl
    );
}
