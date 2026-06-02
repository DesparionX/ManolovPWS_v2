using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Infrastructure.Contracts.Results;
using ManolovPWS_v2.Infrastructure.Persistance;
using ManolovPWS_v2.Infrastructure.Persistance.Entities;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authentication;
using ManolovPWS_v2.Shared.Abstractions.Results;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ManolovPWS_v2.Infrastructure.Contracts.Authentication
{
    public sealed class RefreshTokensService(AppDbContext context) : IRefreshTokensService
    {
        private readonly AppDbContext _context = context;

        public async Task<ITaskResult<RefreshToken>> CreateTokenAsync(UserId userId, CancellationToken cancellationToken = default)
        {
            var rawToken = GenerateRefreshToken();
            var tokenHash = HashRefreshToken(rawToken);

            var expiresAtUtc = DateTime.UtcNow.AddDays(7);

            var dbToken = new DbRefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = userId.Value,
                TokenHash = tokenHash,
                CreatedAtUtc = DateTime.UtcNow,
                ExpiresAtUtc = expiresAtUtc,
            };

            await _context.RefreshTokens.AddAsync(dbToken, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var refreshToken = new RefreshToken(rawToken, expiresAtUtc);

            return Result<RefreshToken>.Success(refreshToken);
        }

        public async Task<ITaskResult<RefreshTokenValidationResult>> ValidateTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                return Result<RefreshTokenValidationResult>.Failure([InfraErrors.RefreshTokenInvalid]);

            var hashToken = HashRefreshToken(refreshToken);

            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(
                   t => t.TokenHash == hashToken,
                    cancellationToken);

            // I'll leave separate checks just in case we want to return different errors
            // for expired vs revoked tokens in the future, but for now they both return the same error.
            if (token is null)
                return Result<RefreshTokenValidationResult>.Failure([InfraErrors.RefreshTokenInvalid]);

            if (token.IsRevoked)
                return Result<RefreshTokenValidationResult>.Failure([InfraErrors.RefreshTokenInvalid]);

            if (token.IsExpired)
                return Result<RefreshTokenValidationResult>.Failure([InfraErrors.RefreshTokenInvalid]);

            return Result<RefreshTokenValidationResult>
                .Success(new RefreshTokenValidationResult(
                    UserId.From(token.UserId.ToString())
                    , hashToken));
        }

        public async Task<ITaskResult> RevokeTokenAsync(string hashedToken, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(hashedToken))
                return Result.Failure([InfraErrors.RefreshTokenInvalid]);

            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(
                   t => t.TokenHash == hashedToken,
                    cancellationToken);

            if (token is null)
                return Result.Failure([InfraErrors.RefreshTokenInvalid]);

            token.RevokedAtUtc = DateTime.UtcNow;

            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        // Helpers
        private static string GenerateRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);

            return Convert.ToBase64String(bytes);
        }
        private static string HashRefreshToken(string refreshToken)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken));

            return Convert.ToBase64String(bytes);
        }
    }
}
