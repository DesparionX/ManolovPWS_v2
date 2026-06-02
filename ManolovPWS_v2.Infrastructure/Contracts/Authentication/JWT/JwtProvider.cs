using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Auth.Token;
using ManolovPWS_v2.Shared.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManolovPWS_v2.Infrastructure.Contracts.Authentication.JWT
{
    public sealed class JwtProvider(IOptions<JwtSettings> options) : ITokenProvider
    {
        private readonly JwtSettings _jwtSettings = options.Value;

        public AccessToken GenerateAccessToken(TokenRequest request)
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

            // Permissions
            if (request.Permissions is not null)
                claims.AddRange(request.Permissions.Select(permission => new Claim(CustomClaimTypes.Permission, permission)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issuer = _jwtSettings.Issuer;
            var audience = _jwtSettings.Audience;
            var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);

            var rawToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(rawToken);

            return new AccessToken(token, expires);
        }
    }
}
