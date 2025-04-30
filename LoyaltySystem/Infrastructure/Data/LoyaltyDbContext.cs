using LoyaltySystem.WebApi.Domain;
using LoyaltySystem.WebApi.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystem.WebApi.Infrastructure.Data;

public class LoyaltyDbContext : DbContext
{
    public LoyaltyDbContext(DbContextOptions<LoyaltyDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}