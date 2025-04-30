using LoyaltySystem.Application;
using Microsoft.Extensions.Caching.Distributed;

namespace LoyaltySystem.Infrastructure;

public class CachingRepository : ICachingRepository
{
    private readonly IDistributedCache _cache;

    public CachingRepository(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task SetUserPointsAsync(int userId, int points)
    {
        var cacheKey = $"user:{userId}:points";
        await _cache.SetStringAsync(cacheKey, points.ToString());
    }
}
