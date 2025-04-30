using LoyaltySystem.WebApi.Application.Services;

namespace LoyaltySystem.WebApi.Presentation.Extensions;

public static class ServiceRegistrant
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }
}
