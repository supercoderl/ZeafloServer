using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Domain
{
    public sealed class ApiUser : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private string? _name;
        private Guid _userId = Guid.Empty;

        public ApiUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Name
        {
            get
            {
                if (_name is not null)
                {
                    return _name;
                }

                var identity = _httpContextAccessor.HttpContext.User.Identity;
                if (identity is null)
                {
                    _name = string.Empty;
                    return string.Empty;
                }

                if (!string.IsNullOrWhiteSpace(identity.Name))
                {
                    _name = identity.Name;
                    return identity.Name;
                }

                var claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => string.Equals(c.Type, ClaimTypes.Name, StringComparison.OrdinalIgnoreCase))?.Value;
                _name = claim ?? string.Empty;
                return _name;
            }
        }

        public string GetEmail()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null || httpContext.User == null)
            {
                return string.Empty; // There is no HttpContext, return empty.
            }

            var claim = httpContext.User.Claims.FirstOrDefault(c => string.Equals(c.Type, ClaimTypes.Email, StringComparison.OrdinalIgnoreCase));

            return !string.IsNullOrWhiteSpace(claim?.Value) ? claim.Value : string.Empty;
        }

        public Guid GetUserId()
        {
            if (_userId != Guid.Empty)
            {
                return _userId;
            }

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null || httpContext.User == null)
            {
                return Guid.Empty; // There is no HttpContext, return empty Guid.
            }

            var claim = httpContext.User.Claims.FirstOrDefault(c => string.Equals(c.Type, ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase));

            if (claim == null || !Guid.TryParse(claim.Value, out var id))
            {
                return Guid.Empty;
            }

            _userId = id;
            return id;
        }

        /*public IEnumerable<UserRole> GetUserRoles()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims.Where(c => string.Equals(c.Type, ClaimTypes.Role)).Select(c => c.Value).ToList();

            if (claims is null || !claims.Any())
            {
                throw new ArgumentException("No roles found for the user.");
            }

            var userRoles = new List<UserRole>();

            foreach (var claim in claims)
            {
                if (Enum.TryParse(claim, out UserRole userRole))
                {
                    userRoles.Add(userRole);
                }
                else
                    throw new ArgumentException($"Could not try parse role {claim}");
            }

            return userRoles;
        }*/
    }
}
