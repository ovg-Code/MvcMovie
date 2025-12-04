using UUIDNext;

namespace ari2._0.Models;

public class Customer
{
    public Guid Id { get; set; } = Uuid.NewDatabaseFriendly(Database.PostgreSql);
    public Guid? ActorsId { get; set; }
    public Guid? CustomerPublicStatusTypesId { get; set; }
    public bool? IsAgentRetention { get; set; }
    public bool? IsLeasing { get; set; }
    public string? OtherData { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool? IsEnabled { get; set; }

    // Navigation properties
    public virtual Actor? Actor { get; set; }
    public virtual CustomerPublicStatusType? CustomerPublicStatusType { get; set; }
}
