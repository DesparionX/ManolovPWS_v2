using ManolovPWS_v2.Infrastructure.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManolovPWS_v2.Infrastructure.Persistance.Configs
{
    public sealed class RefreshTokensConfiguration : IEntityTypeConfiguration<DbRefreshToken>
    {
        public void Configure(EntityTypeBuilder<DbRefreshToken> refreshTokens)
        {
            refreshTokens.ToTable("RefreshTokens");

            refreshTokens.HasKey(rt => rt.Id);

            refreshTokens.Property(rt => rt.TokenHash)
                .IsRequired();

            refreshTokens.HasIndex(rt => rt.Id);

            refreshTokens.HasOne(rt => rt.User)
                .WithMany()
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
