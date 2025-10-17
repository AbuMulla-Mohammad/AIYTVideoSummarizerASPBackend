using AIYTVideoSummarizer.Application.Interfaces.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AIYTVideoSummarizer.Infrastructure.Security
{
    public class JwtTokenProvider : ITokenProvider
    {
        private readonly JwtOptions _options;

        public JwtTokenProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }
        public Task<string> GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretForKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims:claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            var tokenValue = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return Task.FromResult(tokenValue);
        }
    }
}
