using Microsoft.OpenApi.Models;

namespace LoyaltySystem.Presentation.Extensions;

public static class OpenApiInstaller
{
    public static void AddOpenApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Loyalty System", Version = "v1" });
            c.EnableAnnotations();

            c.ResolveConflictingActions(x => x.First());

            // OAuth2 Security Definition
            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                BearerFormat = "JWT",
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{builder.Configuration["Authentication:Authority"]}/authorize"),
                        TokenUrl = new Uri($"{builder.Configuration["Authentication:Authority"]}/oauth/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "OpenID" },
                            { "profile", "User profile" },
                            { "email", "User email" }
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
                    new[] { "openid", "profile", "email" }
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
            c.OAuthClientSecret(builder.Configuration["Authentication:ClientSecret"]);
            c.OAuthAdditionalQueryStringParams(
                new Dictionary<string, string> { { "audience", builder.Configuration["Authentication:Audience"]! } });
            c.OAuthUsePkce();
            c.OAuthScopes("openid", "profile", "email");
        });
    }
}
