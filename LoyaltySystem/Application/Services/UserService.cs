using LoyaltySystem.WebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace LoyaltySystem.WebApi.Application.Services;

public class UserService : IUserService
{
    private readonly LoyaltyDbContext _context;
    private readonly IDistributedCache _cache;

    public UserService(LoyaltyDbContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task EarnPointsAsync(int userId, int points)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) throw new Exception("User not found");

        user.Points += points;
        await _context.SaveChangesAsync();

        // Update Redis cache
        var cacheKey = $"user:{userId}:points";
        await _cache.SetStringAsync(cacheKey, user.Points.ToString());
    }
}
