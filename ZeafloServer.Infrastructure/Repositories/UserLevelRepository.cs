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
    public sealed class UserLevelRepository : BaseRepository<UserLevel, Guid>, IUserLevelRepository
    {
        public UserLevelRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<UserLevel?> GetByUserAsync(Guid userId)
        {
            return await DbSet.Where(x => x.UserId == userId).Include(x => x.MemberShipLevel).SingleOrDefaultAsync();
        }
    }
}
