using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Interfaces.IRepositories
{
    public interface IMemberShipLevelRepository : IRepository<MemberShipLevel, Guid>
    {
    }
}
