using ManolovPWS_v2.Infrastructure.Persistance.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManolovPWS_v2.Infrastructure.Persistance
{
    public class AppDbContext : IdentityDbContext<DbUser, IdentityRole<Guid>, Guid>
    {
        public override DbSet<DbUser> Users { get; set; } = default!;
        public DbSet<DbProject> Projects { get; set; } = default!;
        public DbSet<DbPost> Posts { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Manually renaming the Identity tables
            builder.Entity<IdentityRole<Guid>>()
                .ToTable("UserRoles");

            builder.Entity<IdentityUserClaim<Guid>>()
                .ToTable("UserClaims");

            builder.Entity<IdentityRoleClaim<Guid>>()
                .ToTable("RoleClaims");

            builder.Entity<IdentityUserLogin<Guid>>()
                .ToTable("UserLogins");

            builder.Entity<IdentityUserToken<Guid>>()
                .ToTable("UserTokens");

            builder.Entity<IdentityUserPasskey<Guid>>()
                .ToTable("UserPasskeys");

            // Load entity configurations
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
