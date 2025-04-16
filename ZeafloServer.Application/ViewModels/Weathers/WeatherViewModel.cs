using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Application.ViewModels.Weathers
{
    public sealed class WeatherViewModel
    {
        public double Temperature { get; set; }
        public double WindSpeed { get; set; }
        public string WindDirection { get; set; } = string.Empty;
        public int Humidity { get; set; }

        public static WeatherViewModel FromWeather(
            double temperature,
            double windSpeed,
            string windDirection,
            int humidity
        )
        {
            return new WeatherViewModel
            {
                Temperature = temperature,
                WindSpeed = windSpeed,
                WindDirection = windDirection,
                Humidity = humidity
            };
        }
    }
}
