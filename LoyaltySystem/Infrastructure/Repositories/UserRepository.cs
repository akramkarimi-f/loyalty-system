using LoyaltySystem.Application.Users;
using LoyaltySystem.Domain;
using LoyaltySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystem.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly LoyaltyDbContext _context;

    public UserRepository(LoyaltyDbContext context)
    {
        _context = context;
    }

    public Task<User?> GetByIdAsync(int userId)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
