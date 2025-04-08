using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Domain.Interfaces.IRepositories
{
    public interface IPlaceRepository : IRepository<Place, Guid>
    {
        IQueryable<Place> QueryByCity(Guid cityId);
    }
}
