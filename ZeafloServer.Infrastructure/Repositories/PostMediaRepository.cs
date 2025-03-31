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
    public sealed class PostMediaRepository : BaseRepository<PostMedia, Guid>, IPostMediaRepository
    {
        public PostMediaRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
