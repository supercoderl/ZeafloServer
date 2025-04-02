using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Domain.Interfaces.IRepositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<User?> GetByIdentifierAsync(string identifier);
        Task<IEnumerable<User?>> GetListByPhoneNumber(List<string> phoneNumbers);
        Task<string?> GetQrCodeUrlAsync(Guid userId);
    }
}
