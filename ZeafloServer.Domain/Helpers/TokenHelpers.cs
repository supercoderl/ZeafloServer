using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Commands.Tokens.CreateToken;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Settings;

namespace ZeafloServer.Domain.Helpers
{
    public class Token
    {
        public string AccessToken { get; set; } = string.Empty;
        public long ExpiredTime { get; set; }
    }

    public sealed class TokenHelpers
    {
        public Token BuildToken(User user, TokenSettings tokenSettings, double expiryDurationMinutes)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            };

/*            if (user.UserRoles.Any())
            {
                foreach (var role in user.UserRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleId.ToString()));
                }
            }*/

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret));
            var expiredTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")).AddMinutes(expiryDurationMinutes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new JwtSecurityToken(
                tokenSettings.Issuer,
                tokenSettings.Audience,
                claims,
                expires: expiredTime,
                signingCredentials: credentials
            );

            return new Token
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor),
                ExpiredTime = new DateTimeOffset(expiredTime).ToUnixTimeMilliseconds(),
            };
        }

        public async Task<string?> BuildRefreshToken(User user, string accessToken, IMediatorHandler bus)
        {
            var randomNumber = new Byte[32];
            var randomNumberGenerator = RandomNumberGenerator.Create();

            randomNumberGenerator.GetBytes(randomNumber);
            string refreshToken = Convert.ToBase64String(randomNumber);

            Guid createdCode = await bus.SendCommandAsync(new CreateTokenCommand(
                Guid.NewGuid(), 
                accessToken, 
                refreshToken, 
                user.UserId, 
                false, 
                DateTime.Now.AddDays(45)
            ));

            if (createdCode == Guid.Empty)
            {
                return null;
            }

            return refreshToken;
        }
    }
}
