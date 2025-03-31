using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Messages
{
    public sealed record MessageViewModel(
         Guid MessageId,
         Guid SenderId,
         Guid ReceiverId,
         string Content,
         string? MediaUrl,
         bool IsRead
    );
}
