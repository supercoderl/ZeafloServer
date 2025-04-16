using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Application.ViewModels.Places;
using Microsoft.AspNetCore.Http;

namespace ZeafloServer.Application.Interfaces
{
    public interface IPlaceService
    {
        public Task<PageResult<PlaceViewModel>> GetAllPlacesAsync(
            PageQuery query,
            ActionStatus status,
            List<PlaceType> types,
            string searchTerm = "",
            SortQuery? sortQuery = null
        );
        public Task<PlaceViewModel?> GetPlaceByIdAsync(Guid placeId);
        public Task<Guid> CreatePlaceAsync(CreatePlaceRequest request);
        public Task<List<Guid>> ImportPlacesAsync(ImportPlaceRequest request);
    }
}
