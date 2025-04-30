namespace LoyaltySystem.Application.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ICachingRepository _cachingRepository;

    public UserService(IUserRepository userRepository, ICachingRepository cachingRepository)
    {
        _userRepository = userRepository;
        _cachingRepository = cachingRepository;
    }

    public async Task EarnPointsAsync(int userId, int points)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new Exception("User not found");

        user.Points += points;
        await _userRepository.UpdateAsync(user);
        await _cachingRepository.SetUserPointsAsync(userId, user.Points);
    }
}
