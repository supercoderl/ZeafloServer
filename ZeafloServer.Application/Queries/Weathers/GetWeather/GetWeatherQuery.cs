using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Weathers;

namespace ZeafloServer.Application.Queries.Weathers.GetWeather
{
    public sealed record GetWeatherQuery(double lat, double lon) : IRequest<WeatherViewModel?>;
}
