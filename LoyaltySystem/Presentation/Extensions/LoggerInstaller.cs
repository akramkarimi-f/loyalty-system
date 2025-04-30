using Serilog;

namespace LoyaltySystem.WebApi.Presentation.Extensions;

public static class LoggerInstaller
{
    public static void AddLogger(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((ctx, lc) =>
            lc.ReadFrom.Configuration(ctx.Configuration)
        );
    }

    public static void UseRequestLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
    }
}
