using FluentValidation;
using LoyaltySystem.Presentation.Models;

namespace LoyaltySystem.Presentation.Validators;

public class EarnPointsRequestValidator : AbstractValidator<EarnPointsRequest>
{
    public EarnPointsRequestValidator()
    {
        RuleFor(x => x.Points)
            .GreaterThan(0).WithMessage("Points must be greater than zero.");
    }
}
