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
    public sealed class LikeRepository : BaseRepository<Like, Guid>, ILikeRepository
    {
        public LikeRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
