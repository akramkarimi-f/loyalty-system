using FluentValidation;
using FluentValidation.AspNetCore;
using LoyaltySystem.Presentation.Validators;

namespace LoyaltySystem.Presentation.Extensions;

public static class ValidationInstaller
{
    public static void AddValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<EarnPointsRequestValidator>();
    }
}
