using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Cities;

namespace ZeafloServer.Application.Queries.Cities.GetById
{
    public sealed record GetCityByIdQuery(Guid Id) : IRequest<CityViewModel?>;
}
