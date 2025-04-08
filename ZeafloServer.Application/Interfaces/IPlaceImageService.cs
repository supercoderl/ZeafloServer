using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.PlaceImages;

namespace ZeafloServer.Application.Interfaces
{
    public interface IPlaceImageService
    {
        public Task<Guid> CreatePlaceImageAsync(CreatePlaceImageRequest request);
    }
}
