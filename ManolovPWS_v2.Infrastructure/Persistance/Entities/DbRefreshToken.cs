namespace ManolovPWS_v2.Infrastructure.Persistance.Entities
{
    public sealed class DbRefreshToken
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
        public DbUser User { get; set; } = default!;

        public string TokenHash { get; set; } = default!;

        public DateTime CreatedAtUtc { get; set; }
        public DateTime ExpiresAtUtc { get; set; }

        public DateTime? RevokedAtUtc { get; set; }
        public string? ReplacedByTokenHash { get; set; }

        public bool IsRevoked => RevokedAtUtc is not null;
        public bool IsExpired => DateTime.UtcNow >= ExpiresAtUtc;
        public bool IsActive => !IsRevoked && !IsExpired;
    }
}
