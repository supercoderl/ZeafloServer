using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Infrastructure.EventSourcing
{
    public sealed class EventStoreContext : IEventStoreContext
    {
        private readonly IUser? _user;
        private readonly string _correlationId;

        public EventStoreContext(IUser? user, IHttpContextAccessor? httpContextAccessor)
        {
            _user = user;
            if (httpContextAccessor?.HttpContext is null ||
               !httpContextAccessor.HttpContext.Request.Headers.TryGetValue("X-HEALTH-CARE-CORRELATION-ID", out var id))
            {
                _correlationId = $"internal - {Guid.NewGuid()}";
            }
            else
            {
                _correlationId = id.ToString();
            }
        }

        public string GetCorrelationId()
        {
            return _correlationId;
        }

        public string GetEmail()
        {
            return _user?.GetEmail() ?? string.Empty;
        }
    }
}
