using LoyaltySystem.Domain;

namespace LoyaltySystem.Application.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int userId);
    Task UpdateAsync(User user);
}
