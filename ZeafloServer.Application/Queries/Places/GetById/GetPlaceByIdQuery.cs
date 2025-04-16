using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Places;

namespace ZeafloServer.Application.Queries.Places.GetById
{
    public sealed record GetPlaceByIdQuery(Guid placeId) : IRequest<PlaceViewModel?>;
}
