using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Application.ViewModels.MemberShipLevels;

namespace ZeafloServer.Application.Interfaces
{
    public interface IMemberShipLevelService
    {
        public Task<PageResult<MemberShipLevelViewModel>> GetAllMemberShipLevelsAsync(
            PageQuery query,
            ActionStatus status,
            string searchTerm = "",
            SortQuery? sortQuery = null
        );
    }
}
