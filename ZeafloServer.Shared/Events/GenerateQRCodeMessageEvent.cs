using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Events
{
    public class GenerateQRCodeMessageEvent
    {
        public Guid UserId { get; }

        public GenerateQRCodeMessageEvent(
            Guid userId
        )
        {
            UserId = userId;
        }
    }
}
