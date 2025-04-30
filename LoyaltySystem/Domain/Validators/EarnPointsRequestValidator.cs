using FluentValidation;
using LoyaltySystem.WebApi.Presentation.Models;

namespace LoyaltySystem.WebApi.Domain.Validators;

public class EarnPointsRequestValidator : AbstractValidator<EarnPointsRequest>
{
    public EarnPointsRequestValidator()
    {
        RuleFor(x => x.Points)
            .GreaterThan(0).WithMessage("Points must be greater than zero.");
    }
}
