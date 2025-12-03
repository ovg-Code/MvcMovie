using UUIDNext;

namespace ari2._0.Models;

/// <summary>
/// Representa una relacion entre dos actores (padre-hijo, representante legal, etc).
/// </summary>
public class ActorRelationship
{
    public Guid Id { get; set; } = Uuid.NewDatabaseFriendly(Database.PostgreSql);
    public Guid ParentId { get; set; }
    public Guid ChildId { get; set; }
    public Guid RelationshipTypesId { get; set; }
    public bool? IsPercentage { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool? IsEnabled { get; set; }
}
