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
    public sealed class TokenRepository : BaseRepository<Token, Guid>, ITokenRepository
    {
        public TokenRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<Token?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await DbSet.SingleOrDefaultAsync(x => x.RefreshToken == refreshToken);   
        }
    }
}
