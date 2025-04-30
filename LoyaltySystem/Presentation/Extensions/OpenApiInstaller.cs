using Microsoft.OpenApi.Models;

namespace LoyaltySystem.WebApi.Presentation.Extensions;

public static class OpenApiInstaller
{
    public static void AddOpenApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Loyalty System", Version = "v1" });

            // OAuth2 Security Definition
            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{builder.Configuration["Authentication:Authority"]}authorize"),
                        TokenUrl = new Uri($"{builder.Configuration["Authentication:Authority"]}oauth/token"),
                        Scopes = new Dictionary<string, string>
                {
                    { "openid", "OpenID" },
                    { "profile", "User profile" },
                    { "email", "User email" },
                    { "read:points", "Read points" },
                    { "write:points", "Write points" }
                }
                    }
                }
            });

            // Apply the security requirement globally
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    },
                    new[] { "openid", "profile", "email", "read:points", "write:points" }
                }
            });
        });
    }

    public static void AddOpenApiUI(this WebApplication app, WebApplicationBuilder builder)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Loyalty System v1");
            c.OAuthClientId(builder.Configuration["Authentication:ClientId"]);
            c.OAuthClientSecret(builder.Configuration["Authentication:ClientSecret"]); // Only for public apps if allowed
            c.OAuthUsePkce(); // Use PKCE (recommended for public apps)
            c.OAuthScopes("openid", "profile", "email", "read:points", "write:points");
        });
    }
}
