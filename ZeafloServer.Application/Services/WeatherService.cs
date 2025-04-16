using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.Queries.Weathers.GetWeather;
using ZeafloServer.Application.ViewModels.Weathers;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Application.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IMediatorHandler _bus;

        public WeatherService(
            IMediatorHandler bus
        )
        {
            _bus = bus;
        }

        public async Task<WeatherViewModel?> GetWeatherAsync(double lat, double lon)
        {
            return await _bus.QueryAsync(new GetWeatherQuery(lat, lon));
        }
    }
}
