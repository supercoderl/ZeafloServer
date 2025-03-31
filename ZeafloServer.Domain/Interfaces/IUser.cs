using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
/*        IEnumerable<UserRole> GetUserRoles();*/
        string GetEmail();
    }
}
