using FluentValidation;
using FluentValidation.AspNetCore;
using LoyaltySystem.WebApi.Domain.Validators;

namespace LoyaltySystem.WebApi.Presentation.Extensions;

public static class ValidationInstaller
{
    public static void AddValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<EarnPointsRequestValidator>();
    }
}
