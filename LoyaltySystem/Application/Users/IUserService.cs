namespace LoyaltySystem.Application.Users;

public interface IUserService
{
    Task EarnPointsAsync(int userId, int points);
}