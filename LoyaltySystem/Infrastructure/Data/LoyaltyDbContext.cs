using LoyaltySystem.Domain;
using LoyaltySystem.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystem.Infrastructure.Data;

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