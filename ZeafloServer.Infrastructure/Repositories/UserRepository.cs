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
    public sealed class UserRepository : BaseRepository<User, Guid>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<User?> GetByIdentifierAsync(string identifier)
        {
            return await DbSet.SingleOrDefaultAsync(u => u.Username == identifier || u.Email == identifier);
        }

        public async Task<IEnumerable<User?>> GetListByPhoneNumber(List<string> phoneNumbers)
        {
            return await DbSet.Where(u => phoneNumbers.Contains(u.PhoneNumber)).ToListAsync();
        }

        public async Task<string?> GetQrCodeUrlAsync(Guid userId)
        {
            return await DbSet.Where(u => u.UserId == userId).Select(u => u.QrUrl).SingleOrDefaultAsync();
        }
    }
}
