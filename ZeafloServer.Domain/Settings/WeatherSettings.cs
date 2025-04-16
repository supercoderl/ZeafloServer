using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Settings
{
    public sealed class WeatherSettings
    {
        public string ApiKey { get; set; } = null!;
        public string BaseUrl { get; set; } = null!;
    }
}
