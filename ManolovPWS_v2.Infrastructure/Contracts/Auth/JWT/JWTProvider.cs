using ManolovPWS_v2.Modules.Identity.User.Auth.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManolovPWS_v2.Infrastructure.Contracts.Auth.JWT
{
    public sealed class JwtProvider(IOptions<JwtSettings> options) : ITokenProvider
    {
        private readonly JwtSettings _jwtSettings = options.Value;

        public string GenerateAccessToken(TokenRequest request)
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, request.Id.ToString()),
                new (ClaimTypes.Name, request.UserName),
                new (ClaimTypes.Email, request.Email)
            };

            // Roles
            if (request.Roles is not null)
                claims.AddRange(request.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
