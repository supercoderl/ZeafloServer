using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.ViewModels.PlaceImages;
using ZeafloServer.Domain.Commands.PlaceImages.CreatePlaceImage;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Application.Services
{
    public class PlaceImageService : IPlaceImageService
    {
        private readonly IMediatorHandler _bus;

        public PlaceImageService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<Guid> CreatePlaceImageAsync(CreatePlaceImageRequest request)
        {
            return await _bus.SendCommandAsync(new CreatePlaceImageCommand(
                Guid.NewGuid(),
                request.PlaceId, 
                request.ImageUrl
            ));
        }
    }
}
