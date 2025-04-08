using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Infrastructure.Database;

namespace ZeafloServer.Infrastructure.Repositories
{
    public sealed class PlaceRepository : BaseRepository<Place, Guid>, IPlaceRepository
    {
        public PlaceRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public IQueryable<Place> QueryByCity(Guid cityId)
        {
            return DbSet.Where(p => p.CityId == cityId);
        }
    }
}
