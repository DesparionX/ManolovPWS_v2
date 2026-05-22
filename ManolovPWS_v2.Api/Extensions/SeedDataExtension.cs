using ManolovPWS_v2.Infrastructure.Persistance.Seed;
using Microsoft.AspNetCore.Identity;

namespace ManolovPWS_v2.Api.Extensions
{
    public static class SeedDataExtension
    {
        public static async Task SeedDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            await IdentitySeeder.SeedAsync(roleManager);
        }
    }
}
