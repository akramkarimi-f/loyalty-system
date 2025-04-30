using LoyaltySystem.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogger();

builder.Services.AddDatabase(builder.Configuration);

builder.AddAuthentication();

builder.AddCaching();

builder.Services.AddControllers();
builder.Services.AddValidation();

builder.Services.AddServices();
builder.AddOpenApi();

var app = builder.Build();

// Middleware
app.UseRequestLogging();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


if (app.Environment.IsDevelopment())
{
    app.AddOpenApiUI(builder);
}

await app.SeedDatabase();

app.Run();