using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Weathers;
using ZeafloServer.Domain.Settings;

namespace ZeafloServer.Application.Queries.Weathers.GetWeather
{
    public sealed class GetWeatherQueryHandler : IRequestHandler<GetWeatherQuery, WeatherViewModel?>
    {
        private readonly WeatherSettings _weatherSettings;
        private readonly HttpClient _httpClient;

        public GetWeatherQueryHandler(
            IOptions<WeatherSettings> weatherSettings,
            IHttpClientFactory httpClientFactory
        )
        {
            _weatherSettings = weatherSettings.Value;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<WeatherViewModel?> Handle(GetWeatherQuery request, CancellationToken cancellationToken)
        {
            var url = $"{_weatherSettings.BaseUrl}/weather?lat={request.lat}&lon={request.lon}&appid={_weatherSettings.ApiKey}&units=metric&lang=vi";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var windDeg = root.GetProperty("wind").GetProperty("deg").GetDouble();
            var windSpeed = root.GetProperty("wind").GetProperty("speed").GetDouble();

            return WeatherViewModel.FromWeather(
                root.GetProperty("main").GetProperty("temp").GetDouble(),
                windSpeed,
                GetWindDirection(windDeg),
                root.GetProperty("main").GetProperty("humidity").GetInt32()
            );
        }

        private string GetWindDirection(double deg)
        {
            string[] directions = { "N", "NE", "E", "SE", "S", "SW", "W", "NW", "N" };
            return directions[(int)Math.Round(((deg % 360) / 45))];
        }
    }
}
