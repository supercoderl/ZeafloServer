using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Application.ViewModels.Places
{
    public sealed record ImportPlaceRequest
    (
        IFormFile File,
        string HeadersJSON
    );
}
