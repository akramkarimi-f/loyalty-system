namespace LoyaltySystem.WebApi.Application.Services;

public interface IUserService
{
    Task EarnPointsAsync(int userId, int points);
}