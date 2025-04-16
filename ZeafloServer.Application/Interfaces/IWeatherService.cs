using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Weathers;

namespace ZeafloServer.Application.Interfaces
{
    public interface IWeatherService
    {
        public Task<WeatherViewModel?> GetWeatherAsync(double lat, double lon);
    }
}
