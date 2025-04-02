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
    public sealed class ProcessingRepository : BaseRepository<Processing, Guid>, IProcessingRepository
    {
        public ProcessingRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<Processing?> CheckIsAnyQr(Guid userId)
        {
            return await DbSet.SingleOrDefaultAsync(x  => x.UserId == userId && x.Type == "Upload");
        }

        public async Task<Processing?> GetByUser(Guid userId, string type)
        {
            return await DbSet.SingleOrDefaultAsync(x => x.UserId == userId && x.Type == type);
        }
    }
}
