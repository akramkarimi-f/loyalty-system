using LoyaltySystem.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoyaltySystem.WebApi.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.ExternalId)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(u => u.ExternalId)
               .IsUnique();

        builder.Property(u => u.Name)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(u => u.Points)
               .IsRequired();

        builder.ToTable(t => t.HasCheckConstraint("CK_User_Points_NonNegative", "[Points] >= 0"));
    }
}
