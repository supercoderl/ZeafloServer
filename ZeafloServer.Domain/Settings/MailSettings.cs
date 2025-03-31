using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Settings
{
    public sealed class MailSettings
    {
        public string Sender { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string ServerAddress { get; set; } = null!;
        public int ServerPort { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
