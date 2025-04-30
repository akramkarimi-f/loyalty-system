using LoyaltySystem.WebApi.Domain;
using LoyaltySystem.WebApi.Infrastructure.Configurations;
using LoyaltySystem.WebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LoyaltySystem.WebApi.Presentation.Extensions;

public static class DatabaseInstaller
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LoyaltyDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );

        services.Configure<InitialUserOptions>(configuration.GetSection("InitialUser"));
    }

    public static async Task SeedDatabase(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<LoyaltyDbContext>();
        var config = scope.ServiceProvider.GetRequiredService<IOptions<InitialUserOptions>>();

        await dbContext.Database.EnsureCreatedAsync();

        var initial = config.Value;

        // Seed user if ExternalId not already exists
        if (!dbContext.Users.Any(u => u.ExternalId == initial.ExternalId))
        {
            dbContext.Users.Add(new User
            {
                Name = initial.Name,
                ExternalId = initial.ExternalId,
                Points = 0
            });

            await dbContext.SaveChangesAsync();
            Console.WriteLine($"Seeded initial user with ExternalId: {initial.ExternalId}");
        }
    }
}
