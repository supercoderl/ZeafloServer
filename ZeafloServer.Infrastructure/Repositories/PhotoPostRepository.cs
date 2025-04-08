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
    public sealed class PhotoPostRepository : BaseRepository<PhotoPost, Guid>, IPhotoPostRepository
    {
        public PhotoPostRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
