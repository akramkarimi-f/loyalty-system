namespace LoyaltySystem.Application;

public interface ICachingRepository
{
    Task SetUserPointsAsync(int userId, int points);
}
