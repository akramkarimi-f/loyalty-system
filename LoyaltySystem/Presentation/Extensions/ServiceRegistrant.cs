using LoyaltySystem.Application;
using LoyaltySystem.Application.Users;
using LoyaltySystem.Infrastructure;
using LoyaltySystem.Infrastructure.Repositories;

namespace LoyaltySystem.Presentation.Extensions;

public static class ServiceRegistrant
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICachingRepository, CachingRepository>();
        services.AddScoped<IUserService, UserService>();

    }
}
