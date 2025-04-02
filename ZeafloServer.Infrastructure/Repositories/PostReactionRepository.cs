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
    public sealed class PostReactionRepository : BaseRepository<PostReaction, Guid>, IPostReactionRepository
    {
        public PostReactionRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<PostReaction?> GetByPostAndUserAsync(Guid postId, Guid userId)
        {
            return await DbSet.SingleOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);
        }
    }
}
