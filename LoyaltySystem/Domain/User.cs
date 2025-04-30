namespace LoyaltySystem.WebApi.Domain;

public class User
{
    public int Id { get; set; }
    public required string ExternalId { get; set; }
    public required string Name { get; set; }
    public int Points { get; set; }
}
