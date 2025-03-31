using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Application.ViewModels.Apps
{
    public sealed class AppViewModel
    {
        public AppInformation App { get; set; }
        public UserInformation User { get; set; }
    }

    public sealed class AppInformation
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public sealed class UserInformation
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
    }
}
