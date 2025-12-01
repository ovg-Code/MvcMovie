using UUIDNext;

namespace ari2._0.Models;

public class SocialNetwork
{
    public Guid Id { get; set; } = Uuid.NewDatabaseFriendly(Database.PostgreSql);
    public Guid? ActorsId { get; set; }
    public string? Platform { get; set; }
    public string? ProfileName { get; set; }
    public string? ProfileUrl { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool? IsEnabled { get; set; }
}
