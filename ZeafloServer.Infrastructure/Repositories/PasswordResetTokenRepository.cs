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
    public sealed class PasswordResetTokenRepository : BaseRepository<PasswordResetToken, Guid>, IPasswordResetTokenRepository
    {
        public PasswordResetTokenRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<PasswordResetToken?> GetByCodeAsync(string code)
        {
            return await DbSet.SingleOrDefaultAsync(p => p.Code == code);
        }
    }
}
