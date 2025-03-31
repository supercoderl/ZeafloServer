using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Deepseeks;

namespace ZeafloServer.Application.Interfaces
{
    public interface IDeepseekService
    {
        Task<object?> GenerateResponseAsync(string prompt);
    }
}
