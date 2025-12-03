using UUIDNext;

namespace ari2._0.Models;

/// <summary>
/// Catalogo de tipos de relacion entre actores (representante legal, socio, etc).
/// </summary>
public class RelationshipType
{
    public Guid Id { get; set; } = Uuid.NewDatabaseFriendly(Database.PostgreSql);
    public Guid? ParentId { get; set; }
    public string? Name { get; set; }
    public string? SystemName { get; set; }
    public bool? IsPercentage { get; set; }
    public bool? IsAllowed { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool? IsEnabled { get; set; }
}
