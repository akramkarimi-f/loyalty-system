namespace LoyaltySystem.Presentation.Extensions;

public static class CachingInstaller
{
    public static void AddCaching(this WebApplicationBuilder builder)
    {
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
        });
    }
}
